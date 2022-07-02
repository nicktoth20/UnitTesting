using AutoBogus;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests.Tests.Example_2
{
    public class ExamCalculator
    {
        public bool DidPass(int score)
        {
            return score >= 60 ? true : false;
        }
    }
    
    public class DoNotDoPart2
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