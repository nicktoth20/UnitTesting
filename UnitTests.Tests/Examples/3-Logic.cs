using Bogus;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests.Tests.Examples
{
    /*
     * Another anti-pattern
     * Tests should not contain logic
     * Causes the tests not to be repeatable
     * Logic in the unit tests could be invalid
     *  Would then need unit tests for your unit test
     */

    public class ExamCalculator
    {
        public bool DidPass(int score) => score >= 60;
    }
    
    public class Logic
    {
        [Test]
        public void Should_show_logic_in_test()
        {
            // arrange
            var examScore = new Faker().Random.Number(1, 100);
            
            // act
            var actualResult = new ExamCalculator().DidPass(examScore);

            // assert
            var expected = examScore > 60;
            actualResult.Should().Be(expected);
        }
    }
}