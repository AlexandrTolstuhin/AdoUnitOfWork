using System.Data;

namespace UnitOfWork.Shared
{
    public interface IDbContext
    {
        DbContextState State { get; }

        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        IUnitOfWork UnitOfWork { get; }

        void Commit();

        void Rollback();
    }
}