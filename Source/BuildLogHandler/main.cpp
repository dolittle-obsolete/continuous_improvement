/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
#include <stdio.h>
#include <stdlib.h>
#include <string>
#include <regex>
#include <iostream>

#include "Config.h"

#include "CSharpParser.h"
#include "LogWriter.h"

using namespace std;

int main(int argv, char** args)
{
  if( argv < 3 ) 
  {
    printf("Must provide all expected arguments\n");
    printf("\nUsage:\n");
    printf("BuildLogHandler <stepnumber> <path-to-steps>\n");
    return 1;
  }

  Config config;

  config.StepNumber = atoi(args[1]);
  config.Path = args[2];


  auto originalLine = 0;
  auto logWriter = new LogWriter(config);
  auto structuredResultOutput = new StructuredResultOutput(config);
  Parser* o = new CSharpParser(structuredResultOutput);

  char buffer[1024];
  while (fgets(buffer, sizeof(buffer), stdin))
  {
    logWriter->Write(buffer);
    o->Handle(buffer, originalLine);
    originalLine++;
  }  

  return 0;
}
