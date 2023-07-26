using FluentAssertions;
using Lamar;
using NUnit.Framework;

namespace DIBasics_Lamar.M3; 

[TestFixture]
public class AutoRegistration {
    // смотри M5.MapExample/07.IoC_flexibility 

    [Test]
    public void SimpleDiscovery() {
        var registry = new ServiceRegistry();
        registry.Scan(x => {
                          x.AssemblyContainingType<IWidget>();
                          x.AddAllTypesOf<IWidget>().NameBy(n => n.Name);
                          x.WithDefaultConventions();
                      });

        var container = new Container(registry);

        var instance = container.GetInstance<IWidget>("Odometer");
        instance.Should().NotBeNull();
        instance.Should().BeOfType<Odometer>();
    }

    public interface IWidget { }

    public class Speed : IWidget { }
    public class Odometer : IWidget { }
    public class LastRoute : IWidget { }
}
