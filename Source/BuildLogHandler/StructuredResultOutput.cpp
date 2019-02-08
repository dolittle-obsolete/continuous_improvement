/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
#include <string>
#include "StructuredResultOutput.h"

void StructuredResultOutput::Write(std::string project, std::string file, int line, int column, Severity severity, std::string code, std::string message)
{
    printf("Project : %s\n",project.c_str());
    printf("File : %s\n",file.c_str());
    printf("Position : %d, %d\n",line, column);
    printf("Problem : %d\n", severity);
    printf("Code : %s\n", code.c_str());
    printf("Message : %s\n", message.c_str());
}