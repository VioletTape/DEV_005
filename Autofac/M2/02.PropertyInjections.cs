using Autofac;
using NUnit.Framework;

namespace DIBasics_Autofac.Tests.M2.PI {
    public class PropertyInjections {
        public interface IDependency
        { }

        internal class DefaultDependency : IDependency
        { }

        internal class NonDefaultDependency : IDependency
        {}

        public class CustomService
        {
            public CustomService()
            {
                Dependency = new DefaultDependency();
            }

            public IDependency Dependency { get; set; }
        }

        [TestFixture]
        public class TestPropertyInjection
        {
            [Test]
            public void Test_PropertyInjectionSetter()
            {
                // Arrange
                var builder = new ContainerBuilder();
                builder.RegisterType<NonDefaultDependency>().AsImplementedInterfaces();
                builder
                   .RegisterType<CustomService>()
                   .AsSelf()
                    // инициализация свойства
                   .OnActivated(args => args.Instance.Dependency = args.Context.Resolve<IDependency>());

                // Act
                var container = builder.Build();
                var customService = container.Resolve<CustomService>();
            
                // Assert
                Assert.IsInstanceOf<NonDefaultDependency>(customService.Dependency);
            }
        }
    }
}
