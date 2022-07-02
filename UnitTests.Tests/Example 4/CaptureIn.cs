using System.Collections.Generic;
using AutoBogus;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace UnitTests.Tests.Example_4
{
    public class ExampleRepositoryPart2
    {
        private readonly IDatabase _database;
        
        public ExampleRepositoryPart2(IDatabase database)
        {
            _database = database;
        }
        public void SaveAll(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                _database.Insert(user);
            }
        }
    }

    public class CaptureIn
    {
        [Test]
        public void Should_show_an_example_using_callback()
        {
            // arrange
            var users = AutoFaker.Generate<User>(3);
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<ExampleRepositoryPart2>();
            var actualUsersSaved = new List<User>();
            mocker.GetMock<IDatabase>()
                .Setup(database => database.Insert(It.IsAny<User>()))
                .Callback((User userBeingSaved) => actualUsersSaved.Add(userBeingSaved));

            // act
            sut.SaveAll(users);

            // assert
            actualUsersSaved.Count.Should().Be(3);
            actualUsersSaved[0].Should().Be(users[0]);
            actualUsersSaved[1].Should().Be(users[1]);
            actualUsersSaved[2].Should().Be(users[2]);
        }
        
        [Test]
        public void Should_show_an_example_using_capturein()
        {
            // arrange
            var users = AutoFaker.Generate<User>(3);
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<ExampleRepositoryPart2>();
            var actualUsersSaved = new List<User>();
            mocker.GetMock<IDatabase>()
                .Setup(database => database.Insert(Capture.In(actualUsersSaved)));

            // act
            sut.SaveAll(users);

            // assert
            actualUsersSaved.Count.Should().Be(3);
            actualUsersSaved[0].Should().Be(users[0]);
            actualUsersSaved[1].Should().Be(users[1]);
            actualUsersSaved[2].Should().Be(users[2]);
        }
    }
}
