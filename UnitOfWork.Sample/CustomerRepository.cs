using System.Data;
using System.Threading.Tasks;
using Dapper;
using UnitOfWork.Shared;

namespace UnitOfWork.Sample
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbContext dbContext;

        public CustomerRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private IDbConnection Connection =>
            dbContext.UnitOfWork.Transaction.Connection;

        private IDbTransaction Transaction =>
            dbContext.UnitOfWork.Transaction;

        public Task<Customer> ReadAsync(string id)
        {
            const string query = @"SELECT * FROM Customers WHERE CustomerID = ?id?";

            return Connection.QuerySingleOrDefaultAsync<Customer>(query, new { id }, Transaction);
        }

        public Task UpdateAsync(Customer customer)
        {
            const string query = @"UPDATE Customers SET CompanyName = ?CompanyName?, ContactName = ?ContactName? WHERE CustomerID = ?CustomerID?";

            return Connection.ExecuteAsync(query, customer, Transaction);
        }
    }
}