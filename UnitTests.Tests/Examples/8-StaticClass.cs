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
    
    public class Word
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
        
        [Test]
        public void Should_show_wrapper()
        {
            // arrange
            var mocker = new AutoMocker();
            var expectedDate = DateTime.Now;
            mocker.Use<IDateTime>(dt => dt.Now() == expectedDate);
            var sut = mocker.CreateInstance<TimeClock>();

            // act
            var result = sut.GetCurrentTime();
            
            // assert
            result.Should().Be(expectedDate);
        }
    }

    public interface IDateTime
    {
        DateTime Now();
    }

    public class MyDateTime : IDateTime
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
    //public class TimeClock
    //{
    //    private IDateTime _dateTime;
    //    public TimeClock(IDateTime dateTime)
    //    {
    //        _dateTime = dateTime;
    //    }
    //    public DateTime GetCurrentTime()
    //    {
    //        return _dateTime.Now();
    //    }
    //}
}