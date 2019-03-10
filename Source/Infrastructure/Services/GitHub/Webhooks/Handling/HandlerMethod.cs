/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Concepts;
using Dolittle.Lifecycle;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{

    /// <summary>
    /// A combination of type and method that handles an ActivityPayload
    /// </summary>
    public class HandlerMethod : Value<HandlerMethod>
    {
        /// <summary>
        /// Instaniates an instance of a HandlerMethod
        /// </summary>
        /// <param name="type">Type that handles</param>
        /// <param name="methodInfo">Method on the type that handles</param>
        public HandlerMethod(Type type, MethodInfo methodInfo)
        {
            Type = type;
            Method = methodInfo;
        }

        /// <summary>
        /// The type that handles the ActivityPayload
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// The method that handles that ActivityPayload
        /// </summary>
        public MethodInfo Method { get; set; }

        /// <summary>
        /// Creates Handler methods based on the supplied type
        /// </summary>
        /// <param name="handler">Type to inspect for handler methods</param>
        /// <returns>An enumerable of <see cref="HandlerMethod"> for each method on the supplied type that is a Webhook method</returns>
        public static IEnumerable<HandlerMethod> GetUsableHandlerMethodsFrom(Type handler)
        {
            return handler.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                     .Where(_ => IsAWebHookMethod(_)).Select(m => new HandlerMethod(handler,m));
        }

        private static bool IsAWebHookMethod(MethodInfo methodInfo)
        {
            return methodInfo.ReturnType == typeof(void) && methodInfo.Name == "On" 
                                                && methodInfo.GetParameters().Length == 1 
                                                && methodInfo.GetParameters().All(p => typeof(ActivityPayload).IsAssignableFrom(p.ParameterType));
        }        
    }
}