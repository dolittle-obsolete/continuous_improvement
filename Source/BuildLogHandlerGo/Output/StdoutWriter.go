/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
package Output

import (
	"fmt"

	"github.com/dolittle-platform/continuous_improvement/Source/BuildLogHandlerGo/Config"
)

type stdoutWriter struct {
	config Config.Config
}

func NewStdoutWriter() LogWriter {
	return &stdoutWriter{}
}

func (w *stdoutWriter) Configure(config Config.Config) {
	w.config = config
}

func (w *stdoutWriter) Write(line string) {
	fmt.Println(line)
}
