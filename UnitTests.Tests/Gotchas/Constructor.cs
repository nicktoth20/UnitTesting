using Moq.AutoMock;
using NUnit.Framework;

namespace UnitTests.Tests.Gotchas
{
    public class Constructor
    {
        public class TestDependencyWithDependency
        {
            public TestDependencyWithDependency(string stringDependency)
            {
                
            }
        }

        public class TestClassWithDependency
        {
            public TestClassWithDependency(TestDependencyWithDependency testDependencyWithDependency)
            {
            }

            public void CallMe()
            {

            }
        }

        [Test]
        public void Should_show_issue_with_dependencies()
        {
            var mocker = new AutoMocker();

            var sut = mocker.CreateInstance<TestClassWithDependency>();
            
            sut.CallMe();
        }
    }
}
