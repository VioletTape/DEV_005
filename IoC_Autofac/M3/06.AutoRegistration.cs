using Autofac;
using NUnit.Framework;

namespace IoC_Autofac.M3
{
    interface IDependency
    {}

    class DependencyImpl1 : IDependency
    {}

    class DependencyImpl2 : IDependency
    {}
    
    [TestFixture]
    public class TestAutoRegistration
    {
        [Test]
        public void Test_Register_All_IConnectionStringProvider()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(IDependency).Assembly)
                .Where(t => t.Name.StartsWith("DependencyImpl"))
                .AsImplementedInterfaces();

            var container = builder.Build();

            var connStringProviders = container.Resolve<IEnumerable<IDependency>>();
            Assert.That(connStringProviders.Count(), Is.EqualTo(2));
        }
    }
}