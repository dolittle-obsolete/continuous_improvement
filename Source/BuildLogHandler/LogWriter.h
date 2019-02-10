/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
#include <string>
#include "Config.h"

using namespace std;

class LogWriter
{
private:
    Config _config;

public:
    LogWriter(Config config);
    void Write(string line);
};