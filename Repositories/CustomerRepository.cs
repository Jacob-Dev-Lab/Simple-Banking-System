using System;
using System.Collections.Generic;
using SimpleBankingSystem.Interface;
using SimpleBankingSystem.Utilities;

namespace SimpleBankingSystem.Repo
{
    internal class CustomerRepository : AccountValidator, ICustomerRepository
    {
        private static readonly List<Customer> _customers = [];
        public void Add(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (_customers.Contains(customer))
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
