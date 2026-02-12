using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace TestSimpleBankingSystem.Tests.TestRepos
{
    internal class FakeCustomerRepo : ICustomerRepository
    {
        private readonly List<Customer> _customer = [];
        public void Add(Customer customer)
        {
            _customer.Add(customer);
        }

        public Customer GetCustomerById(Guid customerID)
        {
            return _customer.FirstOrDefault(c => c.CustomerId.Equals(customerID));
        }

        public List<Customer> Load()
        {
            return _customer;
        }

        public void Save()
        {
            //throw new NotImplementedException();
        }
    }
}
