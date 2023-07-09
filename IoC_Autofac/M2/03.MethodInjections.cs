using Autofac;
using NUnit.Framework;

namespace IoC_Autofac.M2 {
    // Задает "стратегию" форматирования отчета
    interface IReportFormatter
    { 
        string GetFormatString();
    }

    class DefaultReportFormatter : IReportFormatter
    {
        public string GetFormatString()
        {
            return "$1";
        }
    }

    class ReportService
    {
        // ReportService
        public static string CreateReport(IReportFormatter reportFormatter, double data)
        {
            return default(string);
        }
    }

    [TestFixture]
    public class TestPartialMethodApplication
    {
        [Test]
        public void Test_Partial_Application()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DefaultReportFormatter>()
                   .AsImplementedInterfaces();

            var container = builder.Build();
            
            Func<IReportFormatter, double, string> createReportWithFormatter = 
                ReportService.CreateReport;
            
            /*
             * В реальности IoC почти никогда не используется для разрешения
             * зависимостей для метода.
             * Делегаты могут инициализироваться в корне приложения и потом
             * использоваться по назначению. 
             */
            Func<double, string> createReport =
                d => createReportWithFormatter(container.Resolve<IReportFormatter>(), d);

            // Теперь можем использовать createReport так:
            string report = createReport(42);
        }
    }
}
