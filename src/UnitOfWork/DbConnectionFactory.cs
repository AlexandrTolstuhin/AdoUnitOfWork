using System;
using System.Data;
using UnitOfWork.Shared;

namespace UnitOfWork
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly Func<IDbConnection> _factory;

        public DbConnectionFactory(Func<IDbConnection> connectionFactory)
        {
            _factory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IDbConnection CreateOpenConnection() => _factory();
    }
}