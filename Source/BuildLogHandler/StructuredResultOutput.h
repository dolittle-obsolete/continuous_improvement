/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
#include <string>
#include "Config.h"

using namespace std;

enum Severity
{
    Warning=1,
    Error,
    Info   
};

class StructuredResultOutput
{
private:
    Config _config;

public:
    StructuredResultOutput(Config config);
    void Write(string project, string file, int line, int column, Severity severity, string code, string message, int originalLine);
};