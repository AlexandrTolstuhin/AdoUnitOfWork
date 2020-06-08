using System;
using System.Data;
using UnitOfWork.Shared;

namespace UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDbTransaction transaction)
        {
            State = UnitOfWorkState.Open;
            Transaction = transaction;
        }

        public UnitOfWorkState State { get; private set; }

        public IDbTransaction Transaction { get; }

        public void Commit()
        {
            try
            {
                Transaction.Commit();
                State = UnitOfWorkState.Committed;
            }
            catch (Exception)
            {
                Transaction.Rollback();
                throw;
            }
        }

        public void Rollback()
        {
            Transaction.Rollback();
            State = UnitOfWorkState.RolledBack;
        }
    }
}