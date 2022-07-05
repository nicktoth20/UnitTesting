using FluentAssertions;
using Moq.AutoMock;
using NUnit.Framework;

namespace UnitTests.Tests.Examples
{
    public class SetupAndTearDown
    {
        private AutoMocker _autoMocker;
        private TestClass _sut;
    
        [SetUp]
        public void SetUp()
        {
            _autoMocker = new AutoMocker();
            _autoMocker.GetMock<TestDependency>()
                .Setup(dependency => dependency.ReturnStringValue())
                .Returns("Expected Result");
            _sut = _autoMocker.CreateInstance<TestClass>();
        }
    
        [Test]
        public void Should_show_an_unit_test_example()
        {
            // act
            var actualResult = _sut.MethodToTest();

            // assert
            actualResult.Should().Be("Expected Result");
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker = null;
        }
    }
}
