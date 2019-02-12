/*---------------------------------------------------------------------------------------------
*  Copyright (c) Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
package Parser

func GetParserByType(parserType string) (LogParser, bool) {
	switch parserType {
	case "csharp":
		return NewCSharpParser(), true
	default:
		return nil, false
	}
}
