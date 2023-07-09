namespace DIBasics_Autofac
{
    interface IRepository
    {
        Person Read(int id);
    }

    class SqlRepository : IRepository
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public SqlRepository(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public Person Read(int id)
        {
            return new Person { Name = "From SqlRepository" };
        }
    }



}