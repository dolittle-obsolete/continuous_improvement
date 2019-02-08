/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
#include <string>

enum Severity
{
    Warning=1,
    Error,
    Info   
};

class StructuredResultOutput
{
public:
    void Write(std::string project, std::string file, int line, int column, Severity severity, std::string code, std::string message);
};