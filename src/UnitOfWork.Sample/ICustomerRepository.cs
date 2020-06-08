using System.Threading.Tasks;

namespace UnitOfWork.Sample
{
    public interface ICustomerRepository
    {
        Task<Customer> ReadAsync(string id);
        Task UpdateAsync(Customer customer);
    }
}