namespace DIBasics_Autofac
{
    interface IConnectionStringProvider
    {
        string GetConnectionString();
    }

    class AppConfigConnectionStringProvider : IConnectionStringProvider
    {
        public string GetConnectionString()
        {
            return "ConnectionString";
        }
    }

    class SimpleConnectionStringProvider : IConnectionStringProvider
    {
        private readonly string _connectionString;

        public SimpleConnectionStringProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}