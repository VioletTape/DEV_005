using System;
using System.IO;
using System.Text;
using Autofac;
using NUnit.Framework;

namespace DIBasics_Autofac.Tests.M2.CI {
    internal interface ICustomDependency {
        void Foo();
    }

    internal class CustomDependencyImpl : ICustomDependency {
        public void Foo() {
            throw new NotImplementedException();
        }
    }


    [TestFixture]
    public class ContainerBasicsTests {
        [Test]
        public void Test_Register_Interface() {
            // Arrange
            var builder = new ContainerBuilder();

            // Регистрация типа как самого себя. При этом это будет Синглтон. 
            builder.RegisterType<CustomDependencyImpl>().AsSelf().SingleInstance();
            var container = builder.Build();

            // Act
            var repository1 = container.Resolve<CustomDependencyImpl>();
            var repository2 = container.Resolve<CustomDependencyImpl>();

            Console.WriteLine(ReferenceEquals(repository1, repository2));
        }

        [Test]
        public void Test_Register_Abstract_Class() {
            // Arrange
            var builder = new ContainerBuilder();

            // Регистрация MemoryStream как Stream
            builder.RegisterType<MemoryStream>().As<Stream>();
            var container = builder.Build();

            // Act
            var stream = (Stream)container.Resolve(typeof(Stream));

            // Assert
            Assert.IsNotNull(stream);
        }

        [Test]
        public void Test_Register_Concrete_Class() {
            var builder = new ContainerBuilder();

            // Зависит от контейнера!
            // Регистрация StringBuilder -> StringBuilder
            builder.RegisterType<StringBuilder>().As<StringBuilder>();
            var container = builder.Build();

            var viewModel = container.Resolve<StringBuilder>();
            Assert.IsNotNull(viewModel);
        }
    }
}
