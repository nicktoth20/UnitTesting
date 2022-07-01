using FluentAssertions;
using Moq.AutoMock;
using NUnit.Framework;

namespace UnitTests.Tests.Example1
{
    public class UnitTestDependency
    {
        // remove virtual for example
        public virtual string ReturnStringValue()
        {
            return "Result from dependency";
        }
    }

    public class UnitTestExample
    {
        private readonly UnitTestDependency _unitTestDependency;

        public UnitTestExample(UnitTestDependency unitTestDependency)
        {
            _unitTestDependency = unitTestDependency;
        }

        public string MethodToTest()
        {
            return _unitTestDependency.ReturnStringValue();
        }
    }

    public class UnitTest
    {
        [Test]
        public void Should_show_an_unit_test_example()
        {
            // arrange
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<UnitTestExample>();
            mocker.GetMock<UnitTestDependency>()
                .Setup(dependency => dependency.ReturnStringValue())
                .Returns("Expected Result");

            // act
            var actualResult = sut.MethodToTest();

            // assert
            actualResult.Should().Be("Expected Result");
        }

        [Test]
        public void Should_show_an_unit_test_example_with_a_different_setup()
        {
            // arrange
            var mocker = new AutoMocker();
            // move this line as an example.
            mocker.Use<UnitTestDependency>(dependency => dependency.ReturnStringValue() == "Expected Result");
            var sut = mocker.CreateInstance<UnitTestExample>();

            // act
            var actualResult = sut.MethodToTest();

            // assert
            actualResult.Should().Be("Expected Result");
        }

        [Test]
        public void Should_show_an_unit_test_example_with_another_setup()
        {
            // arrange
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<UnitTestExample>();
            var unitTestDependency = mocker.GetMock<UnitTestDependency>();
            unitTestDependency
                .Setup(dependency => dependency.ReturnStringValue())
                .Returns("Expected Result");
            
            // act
            var actualResult = sut.MethodToTest();

            // assert
            actualResult.Should().Be("Expected Result");
        }
    }

    public class IntegrationTest
    {
        [Test]
        public void Should_be_an_example_of_an_integration_test()
        {
            // arrange
            var sut = new UnitTestExample(new UnitTestDependency());
            
            // act
            var actualResult = sut.MethodToTest();
            
            // assert
            actualResult.Should().Be("Result from dependency");
        }
    }
}
