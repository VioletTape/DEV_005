using System.Reflection;
using Autofac;
using IoC_Autofac.Something;
using NUnit.Framework;

namespace IoC_Autofac.M3
{
    [TestFixture]
    public class TestParameterOverrides
    {
        [Test]
        public void Test_Constructor_Argument_Override()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SimpleConnectionStringProvider>()
                .WithParameter("connectionString", "Custom Connection String")
                .AsImplementedInterfaces();
            var container = builder.Build();

            var provider = container.Resolve<IConnectionStringProvider>();

            Assert.That(provider.GetConnectionString(), Is.EqualTo("Custom Connection String"));
        }

        [Test]
        public void Test_Constructor_Argument_Override_With_Parameter_Selector()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SimpleConnectionStringProvider>()
                .WithParameter(
                parameterSelector: (ParameterInfo pi, IComponentContext cc) => true,
                valueProvider: (ParameterInfo pi, IComponentContext cc) => "Custom Connection String")
                .AsImplementedInterfaces();
            var container = builder.Build();

            var provider = container.Resolve<IConnectionStringProvider>();

            Assert.That(provider.GetConnectionString(), Is.EqualTo("Custom Connection String"));

        }
        
        [Test]
        public void Test_Override_With_Resolve()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SimpleConnectionStringProvider>()
                .AsImplementedInterfaces();
            var container = builder.Build();

            var provider = container
                .Resolve<IConnectionStringProvider>(
                new PositionalParameter(0, "Custom Connection String"));

            Assert.That(provider.GetConnectionString(), Is.EqualTo("Custom Connection String"));

        }

    }
}