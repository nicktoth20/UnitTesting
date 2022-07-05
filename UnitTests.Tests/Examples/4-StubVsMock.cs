using FluentAssertions;
using Moq.AutoMock;
using NUnit.Framework;

namespace UnitTests.Tests.Examples
{
    public class StubVsMock
    {
        [Test]
        public void Should_show_a_stub()
        {
            // arrange
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<TestClass>();
            var unitTestDependencyStub = mocker.GetMock<TestDependency>();
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
            var sut = mocker.CreateInstance<TestClass>();

            // act
            sut.MethodToTest();

            // assert
            var unitTestDependencyMock = mocker.GetMock<TestDependency>();
            unitTestDependencyMock
                .Verify(dependency => dependency.ReturnStringValue(), Moq.Times.Once);
        }
        
        // Only one mock per test
    }
}