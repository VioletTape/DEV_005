using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Lamar;
using NUnit.Framework;

namespace DIBasics_Lamar.M2; 

[TestFixture]
[SuppressMessage("ReSharper", "CommentTypo")]
public class ConstructorInjection {
    
    /// <summary>
    /// Регистрация объектов с пустым конструктором
    /// </summary>
    [Test]
    public void Simple() {
        // arrange
        var registry = new ServiceRegistry();
        registry.For<CalculationService>().Use<CalculationService>();

        var container = new Container(registry);
        
        /*
         * Конечно, стоит помнить, что получение типов таким образом
         * в реальных приложениях не происходит. Все делается автоматически. 
         */
        // act
        var service = container.GetInstance<CalculationService>();

        // assert
        service.Should().NotBeNull();
        service.Should().BeOfType<CalculationService>();
    }

    /// <summary>
    /// Внедрене зависимостей (другой ссылочный тип) в конструктор 
    /// </summary>
    [Test]
    public void TypeInjection() {
        // arrange
        var registry = new ServiceRegistry();
        registry.For<CoolService>().Use<CoolService>();
        registry.For<CalculationService>().Use<CalculationService>();
        
        var container = new Container(registry);
        
        // act
        var service = container.GetInstance<CoolService>();

        // assert
        service.Should().NotBeNull();
        service.Should().BeOfType<CoolService>();
    }

    /// <summary>
    /// Внедрене зависимостей (элементарные типы) в конструктор 
    /// </summary>
    [Test]
    public void BasicTypeParamsForCtor() {
        // arrange
        var registry = new ServiceRegistry();
        registry.For<DataProvider>().Use<DataProvider>()
                .Ctor<string>().Is("Hello");
        
        var container = new Container(registry);
        
        // act
        var service = container.GetInstance<DataProvider>();

        // assert
        service.Should().NotBeNull();
        service.Should().BeOfType<DataProvider>();
    }
    

    class CalculationService {
        public CalculationService() {
        }
    }

    class CoolService {
        public CoolService(CalculationService service) {
        }
    }

    class DataProvider {
        public DataProvider(string connection) {
        }
    }
}
