using System.Data;

namespace UnitOfWork.Shared
{
    public interface IUnitOfWork
    {
        UnitOfWorkState State { get; }

        IDbTransaction Transaction { get; }

        void Commit();

        void Rollback();
    }
}