using FluentAssertions;
using Lamar;
using NUnit.Framework;

namespace DIBasics_Lamar.M3; 

[TestFixture]
public class MultipleRegistrations {
    [Test]
    public void ResolveByName() {
        var userConfig = "google";

        // arrange
        var registry = new ServiceRegistry();
        registry.For<IMap>().Use<YandexMap>().Named("yandex");
        registry.For<IMap>().Use<GoogleMap>().Named("google");
        registry.For<IMap>().Use<HereMap>().Named("here");

        var container = new Container(registry);

        var instance = container.GetInstance<IMap>(userConfig);

        instance.Should().BeOfType<GoogleMap>();
    }

    [Test]
    public void ResolveWithDelegate() {
        var userConfig = "google";

        // arrange
        var registry = new ServiceRegistry();
        registry.For<IMap>().Use<YandexMap>().Named("yandex");
        registry.For<IMap>().Use<GoogleMap>().Named("google");
        registry.For<IMap>().Use<HereMap>().Named("here");
        registry.For<Display>().Use(d => new Display(d.GetInstance<IMap>(userConfig)));

        var container = new Container(registry);

       

        var instance = container.GetInstance<Display>();

        instance.Map.Should().BeOfType<GoogleMap>();
    }

    [Test]
    public void LastRegistrationWins() {
        // arrange
        var registry = new ServiceRegistry();
        registry.For<IMap>().Use<YandexMap>();
        registry.For<IMap>().Use<GoogleMap>();
        registry.For<IMap>().Use<HereMap>();

        var container = new Container(registry);

        var instance = container.GetInstance<IMap>();

        instance.Should().BeOfType<HereMap>();
    }

    [Test]
    public void LastRegistrationWins2() {
        // arrange
        var registry = new ServiceRegistry();
        registry.For<IMap>().Use<HereMap>();
        registry.For<IMap>().Use<GoogleMap>();
        registry.For<IMap>().Use<YandexMap>();

        var container = new Container(registry);

        var instance = container.GetInstance<IMap>();

        instance.Should().BeOfType<YandexMap>();
    }

    public class Display {
        public IMap Map { get; }

        public Display(IMap map) {
            this.Map = map;
        }
    }

    public interface IMap { }

    public class YandexMap : IMap { }
    public class GoogleMap : IMap { }
    public class HereMap : IMap { }

}

