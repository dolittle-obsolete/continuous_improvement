using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dolittle.Logging;
using Infrastructure.Routing;
using Infrastructure.Services.Github.Webhooks.EventPayloads;
using Infrastructure.Services.Github.Webhooks.Handling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Octokit;
using Octokit.Internal;

namespace Infrastructure.Services.Github.Webhooks
{
    public class Route : ICanHandleRoute
    {
        readonly ILogger _logger;
        readonly IGitHubCredentials _credentials;
        readonly IWebhookCoordinator _coordinator;

        public Route(
            ILogger logger,
            IGitHubCredentials credentials,
            IWebhookCoordinator coordinator
        )
        {
            _logger = logger;
            _credentials = credentials;
            _coordinator = coordinator;
        }

        public async Task Handle(HttpRequest request, HttpResponse response, RouteData routeData)
        {
            // Get interesting headers from GitHub
            var eventType = request.Headers["X-GitHub-Event"].Single();
            var deliveryId = Guid.Parse(request.Headers["X-GitHub-Delivery"].Single());
            var signature = request.Headers["X-Hub-Signature"].Single();

            // Calculate the signed hash while we read the response as a string
            var hmac = new HMACSHA1(_credentials.WebhookSecret);
            using (var cryptoStream = new CryptoStream(request.Body, hmac, CryptoStreamMode.Read))
            using (var reader = new StreamReader(cryptoStream))
            {
                var json = await reader.ReadToEndAsync();
                var hash = "sha1="+BitConverter.ToString(hmac.Hash).Replace("-","").ToLower();

                // Compare calculated hash to signature from GitHub
                if (hash == signature)
                {
                    var handled = MaybeHandleVerifyedWebhookContent(eventType, deliveryId, json);
                    if (!handled)
                    {
                        _logger.Information($"Ignored webhook from GitHub, no handlers for event type {eventType}. DeliveryId {deliveryId}.");
                    }
                }
                else
                {
                    _logger.Warning($"Recieved webhook from GitHub with erroneous signature. Expected {hash}, recieved {signature}. DeliveryId {deliveryId}.");
                    response.StatusCode = StatusCodes.Status401Unauthorized;
                }
            }
        }

        bool MaybeHandleVerifyedWebhookContent(string eventType, Guid deliveryId, string json)
        {
            switch (eventType)
            {
                case "installation":
                    return MaybeHandleWebhookEvent<InstallationEventPayload>(deliveryId, json);
                case "installation_repositories":
                    return MaybeHandleWebhookEvent<InstallationRepositoriesEventPayload>(deliveryId, json);
                case "push":
                    return MaybeHandleWebhookEvent<PushEventPayload>(deliveryId, json);
                case "issues":
                    return MaybeHandleWebhookEvent<IssueEventPayload>(deliveryId, json);
                case "pull_request":
                    return MaybeHandleWebhookEvent<PullRequestEventPayload>(deliveryId, json);
                case "pull_request_review":
                    return MaybeHandleWebhookEvent<PullRequestReviewEventPayload>(deliveryId, json);
                case "pull_request_review_comment":
                    return MaybeHandleWebhookEvent<PullRequestCommentPayload>(deliveryId, json);
                case "commit_comment":
                    return MaybeHandleWebhookEvent<CommitCommentPayload>(deliveryId, json);
                case "create":
                    return MaybeHandleWebhookEvent<CreateEventPayload>(deliveryId, json);
                case "delete":
                    return MaybeHandleWebhookEvent<DeleteEventPayload>(deliveryId, json);
                default:
                    return false;
            }
        }

        bool MaybeHandleWebhookEvent<T>(Guid deliveryId, string json) where T : ActivityPayload
        {
            // Check if there are any handlers listening on this event, if not, no point in deserializing it
            if (_coordinator.WillHandle<T>())
            {
                // Deserialize the payload
                var payload = new SimpleJsonSerializer().Deserialize<T>(json);

                // Call handlers
                _coordinator.HandleWebhookPayload(payload, deliveryId);
                return true;
            }
            else
            {
                // No-one handled the payload
                return false;
            }
        }
    }
}