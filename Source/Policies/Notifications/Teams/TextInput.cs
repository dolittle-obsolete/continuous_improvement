/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Policies.Notifications.Teams
{
    #pragma warning disable 1591
    public class TextInput : Input
    {
        public string @type = "TextInput";
        public bool isMultiline = true;
        public string title;
    }
}