using System.Collections.Generic;
using NUnit.Framework;

namespace UnitTests.Tests.Examples
{
    public class Attributes
    {
        [Test]
        [Repeat(100)]
        public void Should_repeat()
        {
            // Test code
        }

        [Test]
        [Ignore("demo for unit testing")]
        public void Should_ignore()
        {
            // Test code
        }

        [TestCase("Domain")]
        [TestCase("Suppression")]
        [TestCase("General")]
        public void Should_demo_test_case(string type)
        {
            // arrange
            var user = new {Type = type};

            // act

            // assert
        }

        [TestCaseSource(typeof(ExampleSource), nameof(ExampleSource.TypeSelector))]
        public void Should_demo_test_case_source(string type)
        {
            // arrange
            var user = new { Type = type };

            // act

            // assert
        }
    }

    public class ExampleSource
    {
        public static IList<string> TypeSelector = new List<string>()
        {
            "Domain",
            "Suppression",
            "General"
        };
    }
}
