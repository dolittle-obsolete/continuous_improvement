/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Concepts.Configuration
{
    /// <summary>
    /// Represents the unique identifier for a notification channel in the system
    /// </summary>
    public class NotificationChannelId : ConceptAs<Guid>
    {
        /// <summary>
        /// Implicitly convert from <see cref="Guid"/> to <see cref="NotificationChannelId"/>
        /// </summary>
        /// <param name="value"><see cref="Guid"/> to convert from</param>
        public static implicit operator NotificationChannelId(Guid value)
        {
            return new NotificationChannelId { Value = value };
        }
    }
}