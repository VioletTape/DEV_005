using FluentAssertions;
using Lamar;
using NUnit.Framework;

namespace DIBasics_Lamar.M2; 
/*
 * !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
 * свойства не должны использоваться для передачи зависимостей
 */
[TestFixture]
public class PropertyInjection {
    [Test]
    public void DefiningExplicitly() {
        // arrange
        var registry = new ServiceRegistry();
        registry.For<CalculationService>().Use<CalculationService>()
                .Setter<LogPrinter>().Is<LogPrinter>();
        registry.For<LogPrinter>().Use<LogPrinter>();
        
        var container = new Container(registry);
        
        // act
        var service = container.GetInstance<CalculationService>();

        service.Printer.Should().NotBeNull();
    }
    
    /*
     * Немного "загрязняет" чистоту сервисов (доменных)
     * данными по конструированию объекта. Но писать кода
     * заметно меньше, и если свойство изменится, то будут
     * знаки что от этого что-то зависит. А так же не надо
     * будет менять корень инициализации. 
     */
    [Test]
    public void DefiningImplicitlyAsMandatoryPropertyRequirement() {
        // arrange
        var registry = new ServiceRegistry();
        registry.For<CalculationService2>().Use<CalculationService2>();
        registry.For<LogPrinter>().Use<LogPrinter>();
        
        var container = new Container(registry);
        
        // act
        var service = container.GetInstance<CalculationService2>();

        service.Printer.Should().NotBeNull();
    }
    
    [Test]
    public void UsingPropertiesNames() {
        // arrange
        var registry = new ServiceRegistry();
        registry.For<CalculationService3>().Use<CalculationService3>()
                .Setter<LogPrinter>("Console").Is<LogPrinter>();
        registry.For<LogPrinter>().Use<LogPrinter>();
        
        var container = new Container(registry);
        
        // act
        var service = container.GetInstance<CalculationService3>();

        service.Console.Should().NotBeNull();
        service.Remote.Should().BeNull();
    }

    class CalculationService {
        public LogPrinter Printer { get; set; }
    }
    
    class CalculationService2 {
        [SetterProperty]
        public LogPrinter Printer { get; set; }
    }
    
    class CalculationService3 {
        public LogPrinter Remote { get; set; }
        public LogPrinter Console { get; set; }
    }
}

internal class LogPrinter {
}
