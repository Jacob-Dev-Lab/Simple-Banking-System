using System;
using System.Collections.Generic;
using SimpleBankingSystem.Interfaces;
using SimpleBankingSystem.Utilities;

namespace SimpleBankingSystem.Repositories
{
    internal class CustomerRepository : AccountValidator, ICustomerRepository
    {
        private static readonly List<Customer> _customers = [];
        public void Add(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            Customer? existingCustomer = _customers.FirstOrDefault
                (c => c.DateOfBirth == customer.DateOfBirth && c.Email == customer.Email);

            if (existingCustomer != null)
                throw new ArgumentException("Existing Customer");

            _customers.Add(customer);
        }

        public Customer? GetById(Guid customerID)
        {
            return _customers.FirstOrDefault(x => x.CustomerId == customerID);
        }

        public void Save(Customer customer)
        {
            //To be Implemented for persistence
        }
    }
}
