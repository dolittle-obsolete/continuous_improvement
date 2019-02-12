/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
package main

import (
	"encoding/json"
	"fmt"
	"io"
	"os"
	"os/exec"
	"syscall"

	"github.com/dolittle-platform/continuous_improvement/Source/BuildLogHandlerGo/Parser"
)

func main() {
	exitCode := 0
	defer os.Exit(exitCode)

	// Load the config
	rawFile, parsedFile, parser := parseEnvironmentConfig()
	defer parser.Close()
	defer parsedFile.Close()
	defer rawFile.Close()

	// Serialize parsed log messages as lines to the parsed file
	encoder := json.NewEncoder(parsedFile)
	parser.OnMessage(func(message Parser.LogMessage) {
		encoder.Encode(message)
	})

	// Create the writer that writes to both the raw log and the parser
	logWriter := io.MultiWriter(rawFile, parser)

	// Start the actual work
	if len(os.Args) < 2 {
		fmt.Fprintln(logWriter, "[error] Must provide command (and arguments) to run.")
		exitCode = 3
		return
	}

	cmd := exec.Command(os.Args[1], os.Args[2:]...)
	cmd.Stdout = logWriter
	if err := cmd.Run(); err != nil {
		fmt.Fprintf(logWriter, "[error] Provided command failed with: %v\n", err)
		// Try to get the original exit code
		if exitErr, ok := err.(*exec.ExitError); ok {
			if status, ok := exitErr.Sys().(syscall.WaitStatus); ok {
				exitCode = status.ExitStatus()
			}
		}
	}
}

const (
	rawPathEnvName    = "DOLITTLE_BUILD_LOG_RAW_PATH"
	parsedPathEnvName = "DOLITTLE_BUILD_LOG_PARSED_PATH"
	parserEnvName     = "DOLITTLE_BUILD_LOG_PARSER"
)

func parseEnvironmentConfig() (rawFile, parsedFile *os.File, parser Parser.LogParser) {
	var err error

	// Raw log file
	if rawPath, found := os.LookupEnv(rawPathEnvName); !found {
		fmt.Printf("Must set environmental variable '%s'\n", rawPathEnvName)
		os.Exit(0)
	} else if rawFile, err = os.OpenFile(rawPath, os.O_APPEND|os.O_CREATE|os.O_WRONLY, os.ModePerm); err != nil {
		fmt.Printf("Could not open file '%s'. Error %v\n", rawPath, err)
		os.Exit(1)
	}

	// Parsed log file
	if parsedPath, found := os.LookupEnv(parsedPathEnvName); !found {
		fmt.Printf("Must set environmental variable '%s'\n", parsedPathEnvName)
		os.Exit(0)
	} else if parsedFile, err = os.OpenFile(parsedPath, os.O_APPEND|os.O_CREATE|os.O_WRONLY, os.ModePerm); err != nil {
		fmt.Printf("Could not open file '%s'. Error %v\n", parsedPath, err)
		os.Exit(1)
	}

	// Log parser
	if parserType, found := os.LookupEnv(parserEnvName); !found {
		fmt.Printf("Must set environmental variable '%s'\n", parserEnvName)
		os.Exit(0)
	} else if parser, found = Parser.GetParserByType(parserType); !found {
		fmt.Printf("Parser type '%s' not found\n", parserType)
		os.Exit(2)
	}

	return
}
