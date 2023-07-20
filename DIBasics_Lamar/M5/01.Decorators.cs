using FluentAssertions;
using Lamar;
using NUnit.Framework;

namespace IoC_Lamar.M5; 

[TestFixture]
public class Decorators {
    [Test]
    public void METHOD() {
        var container = new Container(_ =>
                                      {
                                          _.For<IWidget>().DecorateAllWith<BoldDecorator>();
    
                                          _.For<IWidget>().Use<Header>();
                                          _.For<IWidget>().Use<SimpleWidget>();
    
                                          _.For<IThing>().Use<Thing>();
                                      });

        var instance = container.GetInstance<IWidget>();
        instance.DoSomething();
        instance.Text.Should().Be("<b>header<\\b>");
    }
}

public class Thing : IThing {
}

public class AWidget : IWidget{
    public void DoSomething() {
        Text = "A";
    }

    public string Text { get; set; }
}

public interface IWidget
{
    void DoSomething();
    string Text { get; set; }
}

public class SimpleWidget : IWidget {
    public string Text { get; set; }
    public void DoSomething() {
        Text = "text";
    }
}

public class BoldDecorator : IWidget {
    public BoldDecorator(IWidget inner) {
        Inner = inner;
    }

    public IWidget Inner { get; }
    
    public void DoSomething() {
        Inner.DoSomething();
        Text = $"<b>{Inner.Text}<\\b>";
    }

    public string Text { get; set; }
}

public class Header : IWidget {
    public void DoSomething() {
        Text = "header";
    }

    public string Text { get; set; }
}

public interface IThing {
}
