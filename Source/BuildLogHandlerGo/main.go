/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"

	"github.com/dolittle-platform/continuous_improvement/Source/BuildLogHandlerGo/Config"
	"github.com/dolittle-platform/continuous_improvement/Source/BuildLogHandlerGo/Input"
	"github.com/dolittle-platform/continuous_improvement/Source/BuildLogHandlerGo/Output"
)

func main() {
	if len(os.Args) < 3 {
		fmt.Println("Must provide all expected arguments")
		fmt.Println("Usage:")
		fmt.Println("BuildLogHandler <stepnumber> <path-to-steps>")
		os.Exit(1)
	}

	if step, err := strconv.Atoi(os.Args[1]); err != nil {
		fmt.Println("Step must be an integer")
		os.Exit(2)
	} else {
		config := Config.Config{
			StepNumber: step,
			Path:       os.Args[2],
		}

		writer := Output.NewStdoutWriter()
		writer.Configure(config)

		structuredOutput := Output.NewStdoutStructuredResultOutput()
		structuredOutput.Configure(config)
		parser := Input.NewCSharpParser(structuredOutput)

		originalLine := 0
		reader := bufio.NewScanner(os.Stdin)
		for reader.Scan() {
			line := reader.Text()
			writer.Write(line)
			parser.Handle(line, originalLine)
			originalLine++
		}

		if err := reader.Err(); err != nil {
			fmt.Println("Error reading from stdin:", err)
			os.Exit(3)
		}
	}
}
