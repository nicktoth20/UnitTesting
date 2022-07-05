using AutoBogus;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace UnitTests.Tests.Examples
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; }
    }
    
    public interface IDatabase
    {
        void Insert(User user);
        User FindOne(string id);
    }
    
    public class ExampleRepository
    {
        private readonly IDatabase _database;
        
        public ExampleRepository(IDatabase database)
        {
            _database = database;
        }

        public void Save(User user)
        {
            _database.Insert(user);

            // part 2
            // if (user.Active) {
            //     _database.Insert(user);
            // }
        }

        public User GetSingleUser(string id)
        {
            return _database.FindOne(id);
        }
    }

    public class ItIs
    {
        [Test]
        public void Should_show_an_example_using_it_isany()
        {
            // arrange
            var user = AutoFaker.Generate<User>();
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<ExampleRepository>();

            // act
            sut.Save(user);

            // assert
            // Discuss why this is not ideal
            mocker.GetMock<IDatabase>()
                .Verify(repo => repo.Insert(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void Should_show_an_example_using_it_isany_in_setup()
        {
            // arrange
            var user = AutoFaker.Generate<User>();
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<ExampleRepository>();
            mocker.GetMock<IDatabase>()
                .Setup(repo => repo.FindOne(It.IsAny<string>()))
                .Returns(user);

            // act
            var actual = sut.GetSingleUser("id");

            // assert
            actual.Should().Be(user);
        }
        
        [Test]
        public void Should_show_the_ideal_example()
        {
            // arrange
            var user = AutoFaker.Generate<User>();
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<ExampleRepository>();

            // act
            sut.Save(user);

            // assert
            mocker.GetMock<IDatabase>()
                .Verify(repo => repo.Insert(user), Times.Once);
        }

        [Test]
        public void Should_show_ideal_example_in_setup()
        {
            // arrange
            var user = AutoFaker.Generate<User>();
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<ExampleRepository>();
            mocker.GetMock<IDatabase>()
                .Setup(repo => repo.FindOne("id"))
                .Returns(user);

            // act
            var actual = sut.GetSingleUser("id");

            // assert
            actual.Should().Be(user);
        }

        [Test]
        public void Should_show_example_when_unable_to_match_all_fields()
        {
            // arrange
            var user = AutoFaker.Generate<User>();
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<ExampleRepository>();

            // act
            sut.Save(user);

            // assert
            mocker.GetMock<IDatabase>()
                .Verify(repo => repo.Insert(
                    It.Is<User>(u => 
                        u.FirstName == user.FirstName && 
                        u.LastName == user.LastName)
                    ), Times.Once);
        }
        
        [Test]
        public void Should_show_an_example_using_rule_for()
        {
            // arrange
            var user = new AutoFaker<User>()
                .RuleFor(u => u.Active, active => true)
                .Generate();
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<ExampleRepository>();

            // act
            sut.Save(user);

            // assert
            mocker.GetMock<IDatabase>()
                .Verify(repo => repo.Insert(user), Times.Once);
        }
    }
}
