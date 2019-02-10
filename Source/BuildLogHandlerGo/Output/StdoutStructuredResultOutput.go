/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
package Output

import (
	"fmt"

	"github.com/dolittle-platform/continuous_improvement/Source/BuildLogHandlerGo/Config"
)

type stdoutStructuredResultOutput struct {
	config Config.Config
}

func NewStdoutStructuredResultOutput() StructuredResultOutput {
	return &stdoutStructuredResultOutput{}
}

func (w *stdoutStructuredResultOutput) Configure(config Config.Config) {
	w.config = config
}

func (w *stdoutStructuredResultOutput) Write(project, file string, line, column int, severity Severity, code, message string, originalLine int) {
	fmt.Println("Project :", project)
	fmt.Println("File :", file)
	fmt.Printf("Position %v, %v\n", line, column)
	fmt.Println("Problem :", severity)
	fmt.Println("Code :", code)
	fmt.Println("Message :", message)
	fmt.Println("OriginalLine :", originalLine)
}
