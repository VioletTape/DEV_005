using Autofac;
using IoC_Autofac.Something;
using NUnit.Framework;

namespace IoC_Autofac.Tests
{
    [TestFixture]
    public class EditPersonViewModelTests
    {
        [Test]
        public void Test_Name_Returns_Correct_Value()
        {
            // Arrange
            var builder = new ContainerBuilder();
            builder.RegisterType<SqlRepository>().AsImplementedInterfaces();
            builder.RegisterType<AppConfigConnectionStringProvider>().AsImplementedInterfaces();
            builder.RegisterType<EditPersonViewModel>().AsSelf();

            var container = builder.Build();
            
            // Act
            var viewModel = container.Resolve<EditPersonViewModel>();

            // Assert
            Assert.That(viewModel.Name, Is.EqualTo("From SqlRepository"));
        }


    }
}