using System;
using FluentAssertions;
using Moq.AutoMock;
using NUnit.Framework;

namespace UnitTests.Tests.Examples
{
    public class TimeClock
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }

    public class DateTimeExample
    {
        [Test]
        public void Should_static_class_issues()
        {
            // arrange
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<TimeClock>();

            // act
            var result = sut.GetCurrentTime();

            // assert
            result.Should().Be(DateTime.Now);
        }

        [Test]
        public void Should_static_class_issues_part_2()
        {
            // arrange
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<TimeClock>();

            // act
            var result = sut.GetCurrentTime();

            // assert
            Assert.That(result, Is.EqualTo(DateTime.Now).Within(1).Seconds);
        }
    }
}
