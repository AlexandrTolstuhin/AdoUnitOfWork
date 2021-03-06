﻿using System.Data;
using System.Threading.Tasks;
using Dapper;
using UnitOfWork.Shared;

namespace UnitOfWork.Sample
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbContext _dbContext;

        public CustomerRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IDbConnection Connection =>
            _dbContext.UnitOfWork.Transaction.Connection;

        private IDbTransaction Transaction =>
            _dbContext.UnitOfWork.Transaction;

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