using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Lamar.Microsoft.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
// Lamar
builder.Host.UseLamar(
                      x => x.For<StringBuilder>().Use<StringBuilder>()
                     );

// Autofac
builder.Host.UseServiceProviderFactory(
                                       new AutofacServiceProviderFactory(
                                                                         x => x.RegisterType<StringBuilder>().AsSelf()
                                                                        )
                                      );
// или вот так можно регистрировать позднее
builder.Host.ConfigureContainer<ContainerBuilder>(x => { x.RegisterType<StringBuilder>().AsSelf(); });


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
