using AutoBogus;
using FluentAssertions;
using Moq.AutoMock;
using Newtonsoft.Json;
using NUnit.Framework;

// jason's blog post

namespace UnitTests.Tests.Examples
{
    //public interface IConvert
    //{
    //    string SerializeObject(User user);
    //}

    public class Converter
    {
        public string ConvertToString(User user)
        {
            return JsonConvert.SerializeObject(user);
        }    
    }
    
    // replace
    public class StaticClass
    {
        [Test]
        public void Should_show_implementation_example()
        {
            // arrange
            var user = AutoFaker.Generate<User>();
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<Converter>();

            // act
            var result = sut.ConvertToString(user);
            
            // assert
            // implementation
            result.Should().Be(JsonConvert.SerializeObject(user));
        }

        [Test]
        public void Should_show_different_example()
        {
            // arrange
            var user = new User
            {
                Active = true,
                FirstName = "Barry",
                LastName = "Allen"
            };
            var mocker = new AutoMocker();
            var sut = mocker.CreateInstance<Converter>();

            // act
            var result = sut.ConvertToString(user);

            // assert
            result.Should().Be("{\"FirstName\":\"Barry\",\"LastName\":\"Allen\",\"Active\":true}");
        }

        // Wrap class
        //[Test]
        //public void Should_show_wrapper_example()
        //{
        //    // arrange
        //    var user = AutoFaker.Generate<User>();
        //    var expectedString = AutoFaker.Generate<string>();
        //    var mocker = new AutoMocker();
        //    mocker.Use<IConvert>(convert => convert.SerializeObject(user) == expectedString);
        //    var sut = mocker.CreateInstance<Converter>();

        //    // act
        //    var result = sut.ConvertToString(user);

        //    // assert
        //    result.Should().Be(expectedString);
        //}
    }
}