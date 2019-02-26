/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using FluentValidation;

namespace Concepts.Improvements
{
    /// <summary>
    /// Validates an LogParserName to make sure it is well formed
    /// </summary>
    public class LogParserNameValidator : AbstractValidator<LogParserName>
    {
        /// <summary>
        /// Instantiates an instance of a <see cref="LogParserNameValidator" />
        /// </summary>
        public LogParserNameValidator()
        {
            RuleFor(_ => _.Value)
                .NotEmpty()
                .WithMessage("The LogParserName cannot be empty");
        }
    }

    /// <summary>
    /// Extensions to make it easier to include Concept validators in Input Validators
    /// </summary>
    public static partial class ValidatorBuilderExtensions 
    {
        /// <summary>
        /// Adds an LogParserNameValidator and a Null Check to an LogParserName
        /// </summary>
        /// <typeparam name="T">Type of the Command</typeparam>
        /// <param name="ruleBuilder">instance of the IRuleBuilder</param>
        /// <param name="isOptional">flag to indicate if the <see cref="LogParserName" /> is optional on the command</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, LogParserName> MustBeAValidLogParserName<T>(this IRuleBuilder<T, LogParserName> ruleBuilder, bool isOptional = false) 
        {
            if(!isOptional)
			    ruleBuilder.NotNull().WithMessage("A LogParserName is required");
            return ruleBuilder.SetValidator(new LogParserNameValidator());
		}
    }       
}
