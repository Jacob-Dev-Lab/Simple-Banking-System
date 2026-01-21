using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Infrastructure.Interfaces;
using SimpleBankingSystem.Infrastructure.Repositories;

namespace SimpleBankingSystem.Application.Service.Account
{
    internal class AccountStatusService(IAccountRepository accountRepository) : IAccountStatusService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        public void ActivateAccount(string accountNumber)
        {
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            account.Activate();
        }

        public void DeActivateAccount(string accountNumber)
        {
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            account.Deactivate();
        }
    }
}
