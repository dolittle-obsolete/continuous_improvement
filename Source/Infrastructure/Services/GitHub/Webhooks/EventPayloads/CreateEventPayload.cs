
using System.Collections.Generic;
using Octokit;

#pragma warning disable 1591
namespace Infrastructure.Services.Github.Webhooks.EventPayloads
{
    
    public class CreateEventPayload : ActivityPayload
    {
        public string Ref { get; protected set; }
        public string RefType { get; protected set; }
        public string MasterBranch { get; protected set; }
        public string Description { get; protected set; }

    }
}
#pragma warning restore 1591