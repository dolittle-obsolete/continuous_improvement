/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
#include <string>
#include "LogWriter.h"

LogWriter::LogWriter(Config config)
{
    _config = config;
}

void LogWriter::Write(std::string line)
{
    printf("%s",line.c_str());
}