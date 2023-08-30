using FluentAssertions;
using FluentAssertions.Extensions;
using Lamar;
using NUnit.Framework;

namespace IoC_Lamar.M2;

public delegate DateTime GetNow();

[TestFixture]
public class Delegates {
    [Test]
    public void Usage() {
        Func<DateTime> now = () => 1.June(2023);

        var daysTo = new Calculator(now).DaysTo(23.September(2023));

        daysTo.Should().Be(114);
    }

    [Test]
    public void Registration() {
        var registry = new ServiceRegistry();
        registry.For<Func<DateTime>>().Use(() => DateTime.Now);
        registry.ForConcreteType<Calculator>();

        var container = new Container(registry);

        var calculator = container.GetInstance<Calculator>();

        calculator.DaysTo(DateTime.Today.AddDays(2))
                  .Should()
                  .Be(2);
    }
}

public class Calculator {
    private readonly Func<DateTime> now;

    public Calculator(Func<DateTime> now) {
        this.now = now;
    }

    public int DaysTo(DateTime date) {
        return (int)(date - now()).TotalDays;
    }
}
