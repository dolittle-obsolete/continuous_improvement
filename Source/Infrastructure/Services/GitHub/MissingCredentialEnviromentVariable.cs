using System;

namespace Infrastructure.Services.Github
{
    public class MissingCredentialEnvironmentVariable : ArgumentException
    {
        public MissingCredentialEnvironmentVariable(string variableName) : base($"The environmental variable '{variableName}' that is required, is not set.")
        {
        }
    }
}