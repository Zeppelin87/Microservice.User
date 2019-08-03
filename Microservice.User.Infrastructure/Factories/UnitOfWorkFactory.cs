using Microservice.User.Infrastructure.Interfaces.Factories;
using Microservice.User.Infrastructure.Interfaces.UnitOfWork;
using System.Data.SqlClient;

namespace Microservice.User.Infrastructure.Factories
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly string _connectionString;

        public UnitOfWorkFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork.UnitOfWork(new SqlConnection(_connectionString));
        }
    }
}
