using Autofac;

namespace DIBasics_Autofac
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<IRepository>().As<SqlRepository>();
            var container = builder.Build();

            var repository = container.Resolve<IRepository>();

        }
    }
}
