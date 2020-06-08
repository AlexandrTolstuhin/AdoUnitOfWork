using System.Data;
using UnitOfWork.Shared;

namespace UnitOfWork
{
    public class DbContext : IDbContext
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IUnitOfWork _unitOfWork;

        public DbContext(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public DbContextState State { get; private set; } = DbContextState.Closed;

        public IDbConnection Connection => _connection ??= OpenConnection();

        public IDbTransaction Transaction => _transaction ??= Connection.BeginTransaction();

        public IUnitOfWork UnitOfWork => _unitOfWork ??= new UnitOfWork(Transaction);

        public void Commit()
        {
            try
            {
                UnitOfWork.Commit();
                State = DbContextState.Committed;
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                Reset();
            }
        }

        public void Rollback()
        {
            try
            {
                UnitOfWork.Rollback();
                State = DbContextState.RolledBack;
            }
            finally
            {
                Reset();
            }
        }

        private IDbConnection OpenConnection()
        {
            State = DbContextState.Open;
            return _connectionFactory.CreateOpenConnection();
        }

        private void Reset()
        {
            Connection?.Close();
            Connection?.Dispose();
            Transaction?.Dispose();

            _connection = null;
            _transaction = null;
            _unitOfWork = null;
        }
    }
}