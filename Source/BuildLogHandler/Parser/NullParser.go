/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
package Parser

type nullParser struct {
	hasWrittenNoLoggerWarning bool
	callback                  LogParserOnMessageCallback
}

func NewNullParser() LogParser {
	return &nullParser{
		hasWrittenNoLoggerWarning: false,
	}
}

func (p *nullParser) OnMessage(callback LogParserOnMessageCallback) {
	p.callback = callback
}

func (p *nullParser) Write(data []byte) (int, error) {
	if !p.hasWrittenNoLoggerWarning && p.callback != nil {
		p.callback(LogMessage{
			Severity: SeverityWarning,
			Message:  "No log parser selected, only raw logs are available",
		})
		p.hasWrittenNoLoggerWarning = true
	}

	// Just throw away all data
	return len(data), nil
}

func (*nullParser) Close() error {
	return nil
}
