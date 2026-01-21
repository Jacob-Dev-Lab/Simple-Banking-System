using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Infrastructure.Interfaces;

namespace SimpleBankingSystem.Application.Service.Customer
{
    internal class CustomerProfileService(ICustomerRepository customerRepository) : ICustomerProfileService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public void UpdateLastName(Guid customerID, string lastname)
        {
            var customer = _customerRepository.GetCustomerById(customerID);
            customer.ChangeLastname(lastname);
            _customerRepository.Save();
        }
        public void UpdateEmailAddress(Guid customerID, string emailAddress)
        {
            var customer = _customerRepository.GetCustomerById(customerID);
            customer.ChangeEmailAddress(emailAddress);
            _customerRepository.Save();
        }
    }
}
