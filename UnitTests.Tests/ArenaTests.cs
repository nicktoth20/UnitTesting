using FluentAssertions;
using NUnit.Framework;
using UnitTesting;
using UnitTesting.DiceRoller;
using UnitTesting.Repository;

namespace UnitTests.Tests
{
    public class Tests
    {
        [Test]
        [Repeat(10)]
        [Ignore("Test")]
        public void Test1()
        {
            var arena = new Arena(new CharactersRepository(), new DiceRoller());
            var winner = arena.Fight(1, 2);
            winner.Should().Be(1);
        }

        [Test]
        public void Test2()
        {
            var tt = new Moq.Mock<TT>();
            var arena = new AA(tt.Object);
            var winner = arena.Help();
            winner.Should().Be(1);
            tt.Verify(t => t.TestMe(), Moq.Times.Once);
        }
    }
    public class AA
    {
        private readonly TT testing;

        public AA(TT testing)
        {
            this.testing = testing;
        }

        public int Help()
        {
            return testing.TestMe();
        }
    }

    public class TT
    {
        public TT()
        {

        }
        public int TestMe()
        {
            return 1;
        }
    }
}