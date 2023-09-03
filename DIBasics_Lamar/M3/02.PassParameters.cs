using FluentAssertions;
using Lamar;
using NUnit.Framework;

namespace DIBasics_Lamar.M3; 

public class PassParameters {
    /// <summary>
    /// Внедрене зависимостей (элементарные типы) в конструктор 
    /// </summary>
    [Test]
    public void BasicTypeParamsForCtor() {
        // arrange
        var registry = new ServiceRegistry();
        registry.For<DataProvider>().Use<DataProvider>()
                .Ctor<string>("connection").Is("Hello")
                .Ctor<string>("def").Is("world");

        var container = new Container(registry);

        // act
        var service = container.GetInstance<DataProvider>();

        // assert
        service.Should().NotBeNull();
        service.Should().BeOfType<DataProvider>();
        service.Connection.Should().Be("Hello");
        service.Def.Should().Be("world");
    }

    class DataProvider {
        public string Connection { get; }
        public string Def { get; }

        public DataProvider(string connection, string def = "") {
            Connection = connection;
            Def = def;
        }
    }
}