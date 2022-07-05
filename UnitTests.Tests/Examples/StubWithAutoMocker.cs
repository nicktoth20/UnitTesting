using FluentAssertions;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace UnitTests.Tests.Examples
{
    public class StubWithAutoMocker
    {
        [Test]
        public void Should_show_getmock_setup()
        {
            // arrange
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<TestClass>();
            mocker.GetMock<TestDependency>()
                .Setup(dependency => dependency.ReturnStringValue())
                .Returns("Expected Result");

            // act
            var actualResult = sut.MethodToTest();

            // assert
            actualResult.Should().Be("Expected Result");
            mocker.VerifyAll(); // Discuss verify all
        }

        [Test]
        public void Should_show_use()
        {
            // arrange
            var mocker = new AutoMocker();
            // move this line as an example.
            mocker.Use<TestDependency>(dependency => dependency.ReturnStringValue() == "Expected Result");
            var sut = mocker.CreateInstance<TestClass>();

            // act
            var actualResult = sut.MethodToTest();

            // assert
            actualResult.Should().Be("Expected Result");
        }

        [Test]
        public void Should_show_setup()
        {
            // arrange
            var mocker = new AutoMocker();
            mocker.Setup<TestDependency, string>(dependency => dependency.ReturnStringValue())
                .Returns("Expected Result");
            var sut = mocker.CreateInstance<TestClass>();

            // act
            var actualResult = sut.MethodToTest();

            // assert
            actualResult.Should().Be("Expected Result");
        }

        [Test]
        public void Should_show_more_readable_example()
        {
            // arrange
            var mocker = new AutoMocker();
            TestDependencyMock
                .Setup(mocker.GetMock<TestDependency>())
                .ReturnsStringValue("Expected Result");
            var sut = mocker.CreateInstance<TestClass>();

            // act
            var actualResult = sut.MethodToTest();

            // assert
            actualResult.Should().Be("Expected Result");
        }

        public class TestDependencyMock
        {
            private readonly Mock<TestDependency> _testDependencyMock;

            private TestDependencyMock(Mock<TestDependency> testDependencyMock)
            {
                _testDependencyMock = testDependencyMock;
            }

            public static TestDependencyMock Setup(Mock<TestDependency> testDependencyMock)
            {
                return new TestDependencyMock(testDependencyMock);
            }

            public TestDependencyMock ReturnsStringValue(string value)
            {
                _testDependencyMock
                    .Setup(dependency => dependency.ReturnStringValue())
                    .Returns(value);
                return this;
            }
        }

        //public class TestDependencyMock : Mock<TestDependency>
        //{
        //    public TestDependencyMock ReturnsStringValue(string value)
        //    {
        //        Setup(dependency => dependency.ReturnStringValue())
        //            .Returns(value);
        //        return this;
        //    }
        //}
    }
}
