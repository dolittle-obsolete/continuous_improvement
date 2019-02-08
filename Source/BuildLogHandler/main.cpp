/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
#include <stdio.h>
#include <stdlib.h>
#include <string>
#include <regex>
#include <iostream>

#include "CSharpParser.h"
#include "LogWriter.h"

using namespace std;

int main()
{
  auto originalLine = 0;
  auto logWriter = new LogWriter();
  auto structuredResultOutput = new StructuredResultOutput();
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
