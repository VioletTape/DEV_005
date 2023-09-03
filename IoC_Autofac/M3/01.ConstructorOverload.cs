using Autofac;
using NUnit.Framework;

namespace IoC_Autofac.M3
{
    class Foo
    {
        public Foo(int i) {}
        public Foo() {}
    }

        [TestFixture]
        public class TestConstructorOverload
        {
            [Test]
            public void Test_Call_Specific_Constructor()
            {
                var builder = new ContainerBuilder();
                // Регистрируем конструктор Foo(int)
                builder.RegisterType<Foo>().UsingConstructor(typeof(int)).AsSelf();
                var container = builder.Build();

                var foo = container.Resolve<Foo>(new PositionalParameter(0, 42));
            }

            [Test]
            public void Test_Call_Parameterless_Constructor()
            {
                var builder = new ContainerBuilder();
                // Регистрируем Foo()
                builder.RegisterType<Foo>().UsingConstructor().AsSelf();
                var container = builder.Build();

                var foo = container.Resolve<Foo>();
        }
    }
}