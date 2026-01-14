using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Interface;
using SimpleBankingSystem.Repo;

namespace SimpleBankingSystem.Service.Customer
{
    internal class CustomerProfileService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;

        public CustomerProfileService(ICustomerRepository customerRepository, IAccountRepository accountRepository)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }

        public void UpdateLastname(Guid accountID, string lastname)
        {
            var customer = _customerRepository.GetById(accountID) ??
                throw new KeyNotFoundException("Customer not found.");

            customer.ChangeLastname(lastname);

            _customerRepository.Save(customer);
        }
    }
}
