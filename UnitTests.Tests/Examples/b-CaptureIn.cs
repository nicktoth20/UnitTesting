using System.Collections.Generic;
using AutoBogus;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace UnitTests.Tests.Examples
{

    public class CaptureIn
    {
        public interface IBetterDatabase
        {
            void Insert(User user);
        }

        public class BetterExampleRepository
        {
            private readonly IBetterDatabase _database;

            public BetterExampleRepository(IBetterDatabase database)
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

        [Test]
        public void Should_show_an_example_using_callback()
        {
            // arrange
            var users = AutoFaker.Generate<User>(3);
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<BetterExampleRepository>();
            var actualUsersSaved = new List<User>();
            mocker.GetMock<IBetterDatabase>()
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
            var sut = mocker.CreateInstance<BetterExampleRepository>();
            var actualUsersSaved = new List<User>();
            mocker.GetMock<IBetterDatabase>()
                .Setup(database => database.Insert(Capture.In(actualUsersSaved)));

            // act
            sut.SaveAll(users);

            // assert
            actualUsersSaved.Count.Should().Be(3);
            // How could this be better?
            actualUsersSaved[0].Should().Be(users[0]);
            actualUsersSaved[1].Should().Be(users[1]);
            actualUsersSaved[2].Should().Be(users[2]);
        }
    }
}
