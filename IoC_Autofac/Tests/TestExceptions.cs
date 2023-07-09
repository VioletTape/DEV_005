using System.Reflection;
using Autofac;
using NUnit.Framework;

namespace IoC_Autofac.Tests
{
    
    class WillThrow
    {
        public WillThrow()
        {
            throw new InvalidOperationException("Oops!");
        }
    }

    [TestFixture]
    public class TestExceptions
    {
       [Test]
        public void Test_Activator_CreateInstanceShouldBeTargetInvocationException()
        {
            Assert.Throws<TargetInvocationException>(() => {
                Activator.CreateInstance<WillThrow>();
            });
        }
        [Test]
        public void Test_With_Container_ShouldBeTargetInvocationException()
        {
            Assert.Throws<Autofac.Core.DependencyResolutionException>(() =>
            {
                var builder = new ContainerBuilder();
                builder.RegisterType<WillThrow>().AsSelf();
                var container = builder.Build();

                var instance = container.Resolve<WillThrow>();
            });
            
        }
        [Test]
        public void Test_With_Container_ShouldBeDependencyResolutionException()
        {
            Assert.Throws<Autofac.Core.DependencyResolutionException>(() =>
            {
                var builder = new ContainerBuilder();
                builder.RegisterType<WillThrow>().AsSelf();
                var container = builder.Build();

                var instance = container.Resolve<WillThrow>();
            });

        }
    }
}