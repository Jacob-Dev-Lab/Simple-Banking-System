using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Interface;
using SimpleBankingSystem.Repo;

namespace SimpleBankingSystem.Service
{
    internal class AccountService : IAccountManager
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;

        public AccountService (ICustomerRepository customerRepository, IAccountRepository accountRepository)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }

        //public void AddAccount(Account account)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddCustomer(Customer customer)
        //{
        //    throw new NotImplementedException();
        //}

        //public Account? GetAccount(string accountNumber)
        //{
        //    throw new NotImplementedException();
        //}

        //public Customer? GetCustomer(Guid customerID)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
