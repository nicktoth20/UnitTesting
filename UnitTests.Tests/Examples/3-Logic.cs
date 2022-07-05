using AutoBogus;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests.Tests.Examples
{
    public class ExamCalculator
    {
        public bool DidPass(int score)
        {
            return score >= 60 ? true : false;
        }
    }
    
    public class Logic
    {
        [Test]
        public void Should_show_logic_in_test()
        {
            // arrange
            var score = AutoFaker.Generate<int>();
            
            // act
            var actualResult = new ExamCalculator().DidPass(score);

            // assert
            var expected = score > 60 ? true : false;
            actualResult.Should().Be(expected);
        }
    }
}