/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
package Output

import (
	"github.com/dolittle-platform/continuous_improvement/Source/BuildLogHandlerGo/Config"
)

type Severity uint

const (
	SeverityWarning Severity = iota
	SeverityError
	SeverityInfo
)

type StructuredResultOutput interface {
	Configure(config Config.Config)
	Write(project, file string, line, column int, severity Severity, code, message string, originalLine int)
}
