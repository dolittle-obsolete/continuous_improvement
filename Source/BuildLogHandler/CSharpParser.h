/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
#include <string>
#include "Parser.h"
#include "StructuredResultOutput.h"

class CSharpParser : public virtual Parser
{
private:
  StructuredResultOutput *_structuredResultOutput;

public:
  CSharpParser(StructuredResultOutput *structuredResultOutput);
  void Handle(std::string input, int originalLine);
};
