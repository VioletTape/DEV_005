using FluentAssertions;
using Lamar;
using NUnit.Framework;

namespace DIBasics_Lamar.M3; 

[TestFixture]
public class NamedInstances {
    [Test]
    public void ResolveByName() {
        var userConfig = "google";

        // arrange
        var registry = new ServiceRegistry();
        registry.For<IMap>().Use<YandexMap>();
        registry.For<IMap>().Use<GoogleMap>();
        registry.For<IMap>().Use<HereMap>();

        var container = new Container(registry);

        var instance = container.GetInstance<IMap>(userConfig);

        instance.Should().BeOfType<GoogleMap>();
    }

    [Test]
    public void ResolveWithDelegate() {
        var userConfig = "google";

        // arrange
        var registry = new ServiceRegistry();
        registry.For<IMap>().Use<YandexMap>();
        registry.For<IMap>().Use<GoogleMap>();
        registry.For<IMap>().Use<HereMap>();
        registry.For<Display>().Use(d => new Display(d.GetInstance<IMap>(userConfig)));

        var container = new Container(registry);

        var instance = container.GetInstance<Display>();

        instance.Map.Should().BeOfType<GoogleMap>();
    }

    public class Display {
        public IMap Map { get; }

        public Display(IMap map) {
            this.Map = map;
        }
    }

    public interface IMap { }

    [InstanceName("yandex")]
    public class YandexMap : IMap { }

    [InstanceName("google")]

    public class GoogleMap : IMap { }

    [InstanceName("here")]

    public class HereMap : IMap { }

}

