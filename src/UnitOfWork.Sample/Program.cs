using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.OleDb;
using System.Threading.Tasks;
using UnitOfWork.Shared;

namespace UnitOfWork.Sample
{
    internal class Program
    {
        private static async Task Main()
        {
            var provider = new ServiceCollection()
                .AddTransient<IDbConnectionFactory>(options =>
                {
                    var builder = new OleDbConnectionStringBuilder
                    {
                        Provider = "SQLNCLI11",
                        DataSource = "(local)",
                        ["Integrated Security"] = "SSPI",
                        ["Initial Catalog"] = "Northwind"
                    };

                    return new DbConnectionFactory(() =>
                    {
                        var conn = new OleDbConnection(builder.ConnectionString);

                        conn.Open();
                        return conn;
                    });
                })
                .AddScoped<IDbContext, DbContext>()
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<CustomerService>()
                .BuildServiceProvider();

            var service = provider.GetService<CustomerService>();
            var customer = await service.ReadAsync("ALFKI");

            Console.WriteLine($"{customer.CustomerID} {customer.CompanyName} {customer.ContactName}");
        }
    }
}