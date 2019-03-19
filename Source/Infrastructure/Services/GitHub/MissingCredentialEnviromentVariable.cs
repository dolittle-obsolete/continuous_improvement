/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;

namespace Infrastructure.Services.Github
{
    /// <summary>
    /// Represents the error where the environmental variable indicating where the credentials are located is missing
    /// </summary>
    public class MissingCredentialEnvironmentVariable : ArgumentException
    {
        /// <summary>
        /// Instantiates a new instance of <see cref="MissingCredentialEnvironmentVariable" />
        /// </summary>
        /// <param name="variableName">The name of the environmental variable</param>
        public MissingCredentialEnvironmentVariable(string variableName) : base($"The environmental variable '{variableName}' that is required, is not set.")
        {
        }
    }
}