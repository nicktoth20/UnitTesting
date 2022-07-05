using Bogus;
using NUnit.Framework;

namespace UnitTests.Tests.Examples
{
    public class FakerExamples
    {
        [Test]
        public void Should_show_examples_using_Faker()
        {
            var faker = new Faker();
            var numberInRange = faker.Random.Number(1, 100);
            var sentence = faker.Lorem.Sentence();
            var url = faker.Internet.Url();
        }

        // https://github.com/bchavez/Bogus#bogus-api-support

        // https://github.com/IntegrateDev/Integrate.UnitTesting
    }
}
