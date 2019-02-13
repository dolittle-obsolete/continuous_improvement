# Build Log Handler [Go]

The purpose of this project is to provide a way to easily handle logs coming during builds. It leverages pipes and takes any STDIN and parses what it gets. It looks at what it gets and tries to parse it to make sense, based on the type of step being built. Raw logs are passed through to a log file and any recognized results, such as compiler warning / errors / info is put into a structured JSON output file.

### Getting started
1. Install Go as described here https://golang.org/doc/install
1. Run `go run . <actual-command> [<actual-command-args>...]` to run the program
    - Make sure the following environmental variables are set:
        1. `DOLITTLE_BUILD_LOG_RAW_PATH` the path to append the raw log
        1. `DOLITTLE_BUILD_LOG_PARSED_PATH` the path to append the structured log
        1. `DOLITTLE_BUILD_LOG_PARSER` the type of the parser to use, currently supported: `csharp`.

An executable can be compiled by running `go build`. To build for another platform set the `GOOS` and `GOARCH` environmental variables, e.g. `GOOS=linux GOARCH=amd64 go build .`. Run `go tool dist list` to get a complete list of `GOOS/GOARCH` combinations.

> The Go extension for VSCode is highly recommended!

##### Debugging
Debugging works out of the box in VSCode with the Go extension installed. However, we need to figure out how to pass logdata into the application to get any use of it. This should probably be done by reading from files while debugging.