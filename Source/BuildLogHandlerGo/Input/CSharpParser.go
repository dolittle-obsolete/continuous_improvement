/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
package Input

import (
	"regexp"
	"strconv"

	"github.com/dolittle-platform/continuous_improvement/Source/BuildLogHandlerGo/Output"
)

var rx = regexp.MustCompile("^([^\\s].*)\\((\\d+),(\\d+)\\):\\s+(error|warning|info)\\s+(CS\\d+)\\s*:\\s*(.*)\\s*\\[(.*)\\]\\n*$")

type cSharpParser struct {
	output Output.StructuredResultOutput
}

func NewCSharpParser(output Output.StructuredResultOutput) Parser {
	return &cSharpParser{
		output: output,
	}
}

func (p *cSharpParser) Handle(input string, originalLine int) {
	if match := rx.FindStringSubmatch(input); match != nil {
		p.output.Write(
			match[7],
			match[1],
			atoiOr(match[2], -1),
			atoiOr(match[3], -1),
			Output.SeverityError,
			match[5],
			match[6],
			originalLine,
		)
	}
}

func atoiOr(str string, or int) int {
	if res, err := strconv.Atoi(str); err == nil {
		return res
	}
	return or
}
