using AutoBogus;
using FluentAssertions;
using Moq.AutoMock;
using NUnit.Framework;

namespace UnitTests.Tests.Gotchas
{
    public class WordTester
    {
        public bool IsUnique(string first, string second)
        {
            return first != second;
        }    
    }
    
    public class Word
    {
        [Test]
        [Repeat(1000)]
        public void Should_fail_eventually()
        {
            // arrange
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<WordTester>();
            var firstWord = AutoFaker.Generate<string>();
            var secondWord = AutoFaker.Generate<string>();

            // act
            var result = sut.IsUnique(firstWord, secondWord);
            
            // assert
            result.Should().BeTrue();
        }
    }
}