using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Interfaces;

namespace SimpleBankingSystem.Service.Customer
{
    internal class CustomerProfileService : ICustomerProfileService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerProfileService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void UpdateLastName(Guid customerID, string lastname)
        {
            var customer = _customerRepository.GetById(customerID);
            customer.ChangeLastname(lastname);
            _customerRepository.Save(customer);
        }
        public void UpdateEmailAddress(Guid customerID, string emailAddress)
        {
            var customer = _customerRepository.GetById(customerID);
            customer.ChangeEmailAddress(emailAddress);
            _customerRepository.Save(customer);
        }
    }
}
