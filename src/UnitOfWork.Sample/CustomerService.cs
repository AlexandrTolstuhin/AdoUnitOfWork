using System.Threading.Tasks;
using UnitOfWork.Shared;

namespace UnitOfWork.Sample
{
    public class CustomerService
    {
        private readonly IDbContext _dbContext;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
            IDbContext dbContext,
            ICustomerRepository customerRepository)
        {
            _dbContext = dbContext;
            _customerRepository = customerRepository;
        }

        public async Task<Customer> ReadAsync(string id)
        {
            var customer = await _customerRepository.ReadAsync(id);
            _dbContext.Commit();

            return customer;
        }

        public async Task UpdateAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
            _dbContext.Commit();
        }
    }
}