/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
package Parser

import (
	"regexp"
	"strconv"
)

var rx = regexp.MustCompile("^([^\\s].*)\\((\\d+),(\\d+)\\):\\s+(error|warning|info)\\s+(CS\\d+)\\s*:\\s*(.*)\\s*\\[(.*)\\]\\n*$")

// NewCSharpParser returns a new LogParser that can deal with CSharp logs
func NewCSharpParser() LogParser {
	return newLineBasedParser(func(line string, lineNumber uint) (LogMessage, bool) {
		if match := rx.FindStringSubmatch(line); match != nil {
			return LogMessage{
				Project:  match[7],
				File:     match[1],
				Line:     atoiOr(match[2], -1),
				Column:   atoiOr(match[3], -1),
				Severity: SeverityError,
				Code:     match[5],
				Message:  match[6],
				RawLine:  int(lineNumber),
			}, true
		}
		return LogMessage{}, false
	})
}

func atoiOr(str string, or int) int {
	if res, err := strconv.Atoi(str); err == nil {
		return res
	}
	return or
}
