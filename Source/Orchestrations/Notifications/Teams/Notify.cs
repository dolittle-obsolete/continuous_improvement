/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Dolittle.Serialization.Json;
using Infrastructure.Orchestrations;

namespace Orchestrations.Notifications.Teams
{
    /// <summary>
    /// Represents a notification performer for Microsoft Teams
    /// </summary>
    public class Notify : IPerformer<Context>
    {
        readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="Notify"/>
        /// </summary>
        /// <param name="serializer">The <see cref="ISerializer"/> to use for Json</param>
        public Notify(ISerializer serializer)
        {
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public bool CanPerform(Context score)
        {
            return true;
        }

        /// <inheritdoc/>
        public async Task Perform(IPerformerLog log, Context score)
        {
            var client = new HttpClient();
            var messageCard = new MessageCard
            {
                title = "Dolittle Build",
                summary = "Build",
                /*
                sections = new Section[] {
                    new Activity {
                        activityTitle = "Blah blah blah",
                        activityText= "[Build .NET Fundamentals...]"
                    },
                    new Facts {
                        title = "Details",
                        facts = new[] {
                            new Fact { name = "Commit", value= "[Blah blah]"},
                            new Fact { name = "Message", value= "[Blah blah]"},
                            new Fact { name = "Duration", value= "[Blah blah]"}
                        }

                    }
                },
                */
                /*
                potentialAction = new[] {
                    new ActionCard {
                        name = "Send feedback",
                        inputs = new [] {
                            new TextInput {
                                id = "feedback",
                                title = "Lets get rumbling..."
                            }
                        },
                        actions = new [] {
                            new HttpPOST {
                                name = "Send feedback",
                                target = "http://www.vg.no"
                            }
                        }
                    }
                }*/
            };

            var json = _serializer.ToJson(messageCard);
            var jsonAsBytes = Encoding.UTF8.GetBytes(json);

            var content = new ByteArrayContent(jsonAsBytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("https://outlook.office.com/webhook/637a02ae-0097-437e-b9b6-a20b2f8731a8@381088c1-de08-4d18-9e60-bbe2c94eccb5/IncomingWebhook/c6cb4389c11b4f77a75580f88a5fc1f6/3ddacd79-85b8-4357-915d-530e1d2b3e0a", content);
            var responseContent = await response.Content.ReadAsStringAsync();
        }
    }
}