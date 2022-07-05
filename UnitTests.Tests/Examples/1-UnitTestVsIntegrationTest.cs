using FluentAssertions;
using Moq.AutoMock;
using NUnit.Framework;

namespace UnitTests.Tests.Examples
{
    public class TestDependency
    {
        // remove virtual for example
        public virtual string ReturnStringValue()
        {
            return "Result from dependency";
        }
    }

    public class TestClass
    {
        private readonly TestDependency _testDependency;

        public TestClass(TestDependency testDependency)
        {
            _testDependency = testDependency;
        }

        public string MethodToTest()
        {
            return _testDependency.ReturnStringValue();
        }
    }

    public class UnitTestVsIntegrationTest
    {
        [Test]
        public void Should_show_an_unit_test_example()
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

        /*
         * Naming conventions:
         *  Book - [UnitOfWorkName]_[ScenarioUnderTest]_[ExpectedBehavior]
         *  Example: MethodToTest_ValidCall_ReturnString
         */

        [Test]
        public void Should_show_an_unit_test_example_with_a_different_setup()
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
        public void Should_show_an_unit_test_example_with_another_setup()
        {
            // arrange
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<TestClass>();
            var unitTestDependency = mocker.GetMock<TestDependency>();
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
            var sut = new TestClass(new TestDependency());
            
            // act
            var actualResult = sut.MethodToTest();
            
            // assert
            actualResult.Should().Be("Result from dependency");
        }
    }
}
