using FluentAssertions;
using Moq.AutoMock;
using NUnit.Framework;
using UnitTests.Tests.Example_1;

namespace UnitTests.Tests.Example_2
{
    public class DoNotDo
    {
        private AutoMocker _autoMocker;
        private UnitTestExample _sut;
    
        [SetUp]
        public void SetUp()
        {
            _autoMocker = new AutoMocker();
            _autoMocker.GetMock<UnitTestDependency>()
                .Setup(dependency => dependency.ReturnStringValue())
                .Returns("Expected Result");
            _sut = _autoMocker.CreateInstance<UnitTestExample>();
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
