using FluentAssertions;
using Lamar;
using NUnit.Framework;

namespace IoC_Lamar.M6; 

[TestFixture]
public class Activators {
    [Test]
    public void ClassActivation() {
        var container = new Container(x =>
                                      {
                                          x.For<Poller>().Use<Poller>()
                                           .OnCreation(poller => poller.Start());
                                      });

        var instance = container.GetInstance<Poller>();
        instance.Text.Should().Be("Started");
    }
}

public class Poller 
{
    public string Text { get; set; }

    public void Start() {
        Text = "Started";
    }
}
