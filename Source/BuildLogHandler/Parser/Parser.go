/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
package Parser

import (
	"io"
)

// Severity indicates the severity of a LogMessage
type Severity uint

// Severity constants
const (
	SeverityInfo Severity = iota
	SeverityWarning
	SeverityError
)

// A LogMessage represents structured log output
type LogMessage struct {
	Project  string
	File     string
	Line     int
	Column   int
	Severity Severity
	Code     string
	Message  string
	RawLine  int
}

// LogParserOnMessageCallback represents a function that will be called when a LogParser has parsed a full LogMessage
type LogParserOnMessageCallback func(LogMessage)

// A LogParser is a type capable of transforming raw log data to structured log output
type LogParser interface {
	io.WriteCloser
	OnMessage(LogParserOnMessageCallback)
}
