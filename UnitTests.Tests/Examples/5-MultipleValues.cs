using FluentAssertions;
using Moq.AutoMock;
using NUnit.Framework;

namespace UnitTests.Tests.Examples
{
    public interface INameGenerator
    {
        string Generate();
    }
    public class UserCreator
    {
        private INameGenerator _nameGenerator;
        
        public UserCreator(INameGenerator nameGenerator)
        {
            _nameGenerator = nameGenerator;
        }

        public User Create()
        {
            return new User
            {
                FirstName = _nameGenerator.Generate(),
                LastName = _nameGenerator.Generate()
            };
        }
    }

    public class MultipleValues
    {
        [Test]
        public void ReturnMultipleValues()
        {
            // arrange
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<UserCreator>();
            mocker.GetMock<INameGenerator>()
                .SetupSequence(generator => generator.Generate())
                .Returns("First")
                .Returns("Second");
            
            // act
            var user = sut.Create();
            
            // assert
            user.FirstName.Should().Be("First");
            user.LastName.Should().Be("Second");
        }
    }
}
