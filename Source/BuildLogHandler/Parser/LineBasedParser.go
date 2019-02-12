/*---------------------------------------------------------------------------------------------
*  Copyright (c) Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
package Parser

import (
	"bufio"
	"io"
)

// A LineParser converts a line of a log into structured LogMessages
type LineParser func(line string, lineNumber uint) (LogMessage, bool)

type lineBasedParser struct {
	*io.PipeWriter
	callback LogParserOnMessageCallback
}

func newLineBasedParser(lineParser LineParser) LogParser {
	parser := lineBasedParser{}

	reader, writer := io.Pipe()
	scanner := bufio.NewScanner(reader)

	parser.PipeWriter = writer

	go func() {
		var lineNumber uint
		for scanner.Scan() {
			message, parsed := lineParser(scanner.Text(), lineNumber)
			if parsed && parser.callback != nil {
				parser.callback(message)
			}
			lineNumber++
		}
	}()

	return &parser
}

func (p *lineBasedParser) OnMessage(callback LogParserOnMessageCallback) {
	p.callback = callback
}
