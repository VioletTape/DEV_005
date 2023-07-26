using FluentAssertions;
using Lamar;
using NUnit.Framework;

namespace IoC_Lamar.M6;

[TestFixture]
public class Interceptors {
    [Test]
    public void ApplyInterceptors() {
        var container = new Container(_ => {
                                          _.For<IWidget>().Use<Header>()
                                           .Named("Header")
                                           .OnCreation(w => new BoldDecorator(w));

                                          _.For<IWidget>().Use<SimpleWidget>()
                                           .Named("Plain");
                                      }
                                     );


        var header = container.GetInstance<IWidget>("Header");
        header.DoSomething();
        header.Text.Should().Be("<b>header<\\b>");

        var text = container.GetInstance<IWidget>("Plain");
        text.DoSomething();
        text.Text.Should().Be("text");
    }
}
