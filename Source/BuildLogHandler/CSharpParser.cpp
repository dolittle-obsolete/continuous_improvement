/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
#include <string>
#include <regex>
#include "CSharpParser.h"

using namespace std;

string expression = "^([^\\s].*)\\((\\d+),(\\d+)\\):\\s+(error|warning|info)\\s+(CS\\d+)\\s*:\\s*(.*)\\s*\\[(.*)\\]\\n*$";

auto rx = regex(expression);

CSharpParser::CSharpParser(StructuredResultOutput *structuredResultOutput)
{
  _structuredResultOutput = structuredResultOutput;
}

void CSharpParser::Handle(std::string input, int originalLine)
{

  cmatch cm;
  auto match = regex_match(input.c_str(), cm, rx);
  if (match)
  {
    auto file = cm[1].str();
    auto line = atoi(cm[2].str().c_str());
    auto column = atoi(cm[3].str().c_str());
    auto severity = cm[4].str();
    auto code = cm[5].str();
    auto message = cm[6].str();
    auto project = cm[7].str();

    _structuredResultOutput->Write(project, file, line, column, Severity::Error, code, message, originalLine);
  }
};
