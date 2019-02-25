/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using Concepts;
using Xunit;

namespace Concepts.Specs.for_Version
{
    /// <summary>
    /// Using Xunit as these are more parameterized unit tests than behavioural specs
    /// </summary>
    public class when_validating_a_version
    {
        private readonly VersionValidator _validator;

        public when_validating_a_version()
        {
            _validator = new VersionValidator();
        }   

        [Theory]
        [InlineData("1.1.1")] 
        [InlineData("100.100.100")] 
        [InlineData("12.13.14-abcd")] 
        [InlineData("12.13.14-00012")] 
        public void should_be_valid_when_the_version_string_is_valid(string version)
        {
            var isValid = _validator.Validate(version).IsValid;
            Assert.True(isValid);
        }

        [Theory]
        [InlineData(null)] 
        [InlineData("")] 
        [InlineData("1")] 
        [InlineData("1.1")] 
        [InlineData("1.1.")] 
        [InlineData("1.1.1.")]
        [InlineData("1.1.1-")] 
        public void should_be_invalid_when_the_version_string_is_invalid(string version)
        {
            var isValid = _validator.Validate(version).IsValid;
            Assert.False(isValid);
        }
    }
}