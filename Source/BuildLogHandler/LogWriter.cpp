/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
#include <stdio.h>
#include <string>
using namespace std;

#include "LogWriter.h"

LogWriter::LogWriter(Config config)
{
    _config = config;

    //auto path << "blah";

    //auto path = config.Path+"/"+config.StepNumber+".log";

    //_file = fopen(path.)
}

void LogWriter::Write(std::string line)
{

    printf("%s", line.c_str());
}