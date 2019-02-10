/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
package Output

import (
	"github.com/dolittle-platform/continuous_improvement/Source/BuildLogHandlerGo/Config"
)

type LogWriter interface {
	Configure(config Config.Config)
	Write(line string)
}
