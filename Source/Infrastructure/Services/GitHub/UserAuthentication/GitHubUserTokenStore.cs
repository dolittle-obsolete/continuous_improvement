using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Dolittle.Execution;
using Dolittle.Lifecycle;
using Dolittle.Tenancy;
using Octokit;

namespace Infrastructure.Services.Github.UserAuthentication
{
    [SingletonPerTenant]
    public class GitHubUserTokenStore : IGitHubUserTokenStore
    {
        readonly RNGCryptoServiceProvider _generator;
        readonly IExecutionContextManager _executionContextManager;

        // TODO: Store differently per user based on claims or something
        bool _hasUserToken;
        OauthToken _userToken;
        string _authState;
        string _callback;

        public GitHubUserTokenStore(
            IExecutionContextManager executionContextManager
        )
        {
            _executionContextManager = executionContextManager;

            _generator = new RNGCryptoServiceProvider();
        }        

        public bool HasTokenForCurrentUser()
        {
            return _hasUserToken;
        }

        public OauthToken GetTokenForCurrentUser()
        {
            return _userToken;
        }

        public string GenerateStateForCurrentUser(string callback)
        {
            _callback = callback;

            // Generate random state
            var state = new byte[20];
            _generator.GetBytes(state);
            _authState = BitConverter.ToString(state).Replace("-","").ToLower();

            return _authState;
        }

        public string SetTokenForCurrentUser(string state, OauthToken token)
        {
            if (state == _authState) {
                _userToken = token;
                _hasUserToken = true;
                return _callback;
            } else {
                throw new Exception("OAUTH GITHUB STATE DOESNT MATCH");
            }
        }
    }
}