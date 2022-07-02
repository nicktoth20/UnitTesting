using FluentAssertions;
using Moq.AutoMock;
using NUnit.Framework;
using UnitTests.Tests.Example_1;

namespace UnitTests.Tests.Example_3
{
    public class StubVsMock
    {
        [Test]
        public void Should_show_a_stub()
        {
            // arrange
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<UnitTestExample>();
            var unitTestDependencyStub = mocker.GetMock<UnitTestDependency>();
            unitTestDependencyStub
                .Setup(dependency => dependency.ReturnStringValue())
                .Returns("Expected Result");

            // act
            var actualResult = sut.MethodToTest();

            // assert
            actualResult.Should().Be("Expected Result");
        }
        
        [Test]
        public void Should_show_a_mock()
        {
            // arrange
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<UnitTestExample>();

            // act
            sut.MethodToTest();

            // assert
            var unitTestDependencyMock = mocker.GetMock<UnitTestDependency>();
            unitTestDependencyMock
                .Verify(dependency => dependency.ReturnStringValue(), Moq.Times.Once);
        }
        
        // Only one mock per test
    }
}