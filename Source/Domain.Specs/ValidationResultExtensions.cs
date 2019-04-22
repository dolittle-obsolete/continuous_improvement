/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Validation;
using System.Linq;
using System.Collections.Generic;
using Machine.Specifications;
using fv = FluentValidation.Results;

namespace Domain.Specs
{
    /// <summary>
    /// Extensions to make it easier to assert against a <see cref="ValidationResult"> validation results </see>
    /// </summary>
    public static class ValidationResultExtensions
    {
        const string conceptEnding = ".Value";
        public static void ShouldBeValid(this IEnumerable<ValidationResult> validationResults)
        {
            validationResults.Any().ShouldBeFalse();
        }

        public static void ShouldBeInvalid(this IEnumerable<ValidationResult> validationResults)
        {
            validationResults.Any().ShouldBeTrue();
        }

        public static void ShouldHaveInvalidProperty(this IEnumerable<ValidationResult> validationResults, string propertyName)
        {
            validationResults.Any(r => r.MemberNames.Any(n => n == propertyName) || r.MemberNames.Any(n => n == propertyName + conceptEnding)).ShouldBeTrue();
        }

        public static void ShouldHaveInvalidCountOf(this IEnumerable<ValidationResult> validationResults, int expected)
        {
            validationResults.Count().ShouldEqual(expected);
        }

        public static void ShouldBeValid(this fv.ValidationResult validationResults)
        {
            validationResults.IsValid.ShouldBeTrue();
        }

        public static void ShouldBeInvalid(this fv.ValidationResult validationResults)
        {
            validationResults.IsValid.ShouldBeFalse();
        }

        public static void ShouldHaveInvalidProperty(this fv.ValidationResult validationResults, string propertyName)
        {
            validationResults.Errors.Any(r => r.PropertyName == propertyName).ShouldBeTrue();
        }

        public static void ShouldHaveInvalidNestedProperty(this fv.ValidationResult validationResults, string propertyName)
        {
            validationResults.Errors.Any(r => r.PropertyName.EndsWith(propertyName) || r.PropertyName.EndsWith(propertyName + conceptEnding)).ShouldBeTrue();
        }

        public static void ShouldHaveInvalidCountOf(this fv.ValidationResult validationResults, int expected)
        {
            System.Console.WriteLine(validationResults.Errors.Count());
            validationResults.Errors.Count().ShouldEqual(expected);
        }
    }
}
