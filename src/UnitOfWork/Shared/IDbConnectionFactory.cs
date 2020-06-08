using System.Data;

namespace UnitOfWork.Shared
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateOpenConnection();
    }
}