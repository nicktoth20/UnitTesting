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
        /*
         * Unit Test should follow the FIRST principles
         * Fast
         * Isolated / Independent (running individually)
         * Repeatable (different environments; different times of the day)
         * Self-validating (shouldn't have to check manually if the test passed)
         * Timely (written before code)
         */

        /*
         * Naming conventions:
         *  Book - [UnitOfWorkName]_[ScenarioUnderTest]_[ExpectedBehavior]
         *  Example: MethodToTest_ValidMethodCall_ReturnString
         */

        [Test]
        public void Should_show_a_unit_test_example()
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
    }

    //public class IntegrationTest
    //{
    //    [Test]
    //    public void Should_be_an_example_of_an_integration_test()
    //    {
    //        // arrange
    //        var sut = new TestClass(new TestDependency());
            
    //        // act
    //        var actualResult = sut.MethodToTest();
            
    //        // assert
    //        actualResult.Should().Be("Result from dependency");
    //    }
    //}
}
