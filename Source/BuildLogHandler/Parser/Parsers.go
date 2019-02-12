/*---------------------------------------------------------------------------------------------
*  Copyright (c) Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
package Parser

func GetParserByType(parserType string) LogParser {
	switch parserType {
	case "csharp":
		return NewCSharpParser()
	default:
		return NewNullParser()
	}
}
