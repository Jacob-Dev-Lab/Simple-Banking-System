using SimpleBankingSystem.Utilities;
using SimpleBankingSystem.Infrastructure.Interfaces;
using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    public class CustomerRepository : AccountValidator, ICustomerRepository
    {
        private static readonly List<Customer> _customers = [];
        public void Add(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            var existingCustomer = _customers.FirstOrDefault
                (c => c.DateOfBirth == customer.DateOfBirth && c.Email == customer.Email);

            if (existingCustomer != null)
                throw new InvalidOperationException("Customer already exist");

            _customers.Add(customer);
        }

        public Customer GetCustomerById(Guid customerID)
        {
            return _customers.FirstOrDefault(x => x.CustomerId == customerID) ??
                throw new KeyNotFoundException("Customer does not exist/incorrect customer information");
        }

        public void Save()
        {
            //To be Implemented for persistence
        }

        public List<Customer> Load()
        {
            //To be Implemented for persistence
            return [];
        }
    }
}
