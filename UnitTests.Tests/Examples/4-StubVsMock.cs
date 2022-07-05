using FluentAssertions;
using Moq.AutoMock;
using NUnit.Framework;

namespace UnitTests.Tests.Examples
{
    public class StubVsMock
    {
        /*
         * Difference:
         *  Stubs cannot be verified against whereas mocks can
         *
         * You should only have one mock per test and only when it makes sense
         *  If you method returns a value, test against that value instead of verifying against a mock
         *
         * You can have multiple stubs per test
         */

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
    }
}