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
        public void Test1()
        {
            var arena = new Arena(new CharactersRepository(), new DiceRoller());
            var winner = arena.Fight(1, 2);
            winner.Should().Be(1);
        }
    }
}