using FluentAssertions;
using NUnit.Framework;
using OnlineCatalog.Common.Extensions;

namespace OnlineCatalog.Common.Tests.Unit.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase(null)]
        [TestCase("")]
        public void WhenStringIsNullOrEmptyThenReturnTrue(string s)
        {
            bool result = s.IsNullOrEmpty();

            result.Should().BeTrue();
        }

        [Test]
        public void WhenStringIsNotNullAndNotEmptyThenReturnFalse()
        {
            string nonEmptyString = "nonEmptyString";
            bool result = nonEmptyString.IsNullOrEmpty();

            result.Should().BeFalse();
        }
    }
}
