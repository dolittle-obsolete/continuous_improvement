# Build Log Handler [Go]

A quick rewrite of the C++ Build Log Handler if we want to play with Golang for this application. See [other README](../BuildLogHandler/README.md) for details.

### Getting started
1. Install Go as described here https://golang.org/doc/install
1. Run `go run . <step> <path-to-steps>` to run the program

An executable can be compiled by running `go build`

> The Go extension for VSCode is highly recommended!

##### Debugging
Debugging works out of the box in VSCode with the Go extension installed. However, we need to figure out how to pass logdata into the application to get any use of it. This should probably be done by reading from files while debugging.