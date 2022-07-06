using AutoBogus;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace UnitTests.Tests.Examples
{
    public class CallBaseExample
    {
        private readonly MangoDb _database;
        private int _usersSaved = 0;

        public CallBaseExample()
        {
            _database = new MangoDb();
        }

        public int SaveUserAndIncrementCount(User user)
        {
            SaveUser(user);
            IncrementCount();
            return _usersSaved;
        }

        public virtual void SaveUser(User user)
        {
            _database.Save(user);
        }

        public virtual void IncrementCount()
        {
            _usersSaved++;
        }
    }
    public class CallBase
    {
        [Test]
        public void Should_demo_callbase_usage()
        {
            // arrange
            var user = AutoFaker.Generate<User>();
            var callBaseExample = new Mock<CallBaseExample>()
            {
                CallBase = true
            };
            callBaseExample
                .Setup(example => example.SaveUser(user))
                .Callback(() => {});
            
            // act
            var actual = callBaseExample.Object.SaveUserAndIncrementCount(user);

            // assert
            actual.Should().Be(1);
        }
    }

    public class MangoDb
    {
        public void Save(User user)
        {

        }
    }
}
