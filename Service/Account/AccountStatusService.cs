using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Interfaces;

namespace SimpleBankingSystem.Service.Account
{
    internal class AccountStatusService : IAccountStatusService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountStatusService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void ActivateAccount(string accountNumber)
        {
            var account = _accountRepository.GetByNumber(accountNumber);

            if (account == null)
                throw new KeyNotFoundException("Wrong account number" + nameof(accountNumber));

            account.Activate();
        }

        public void DeActivateAccount(string accountNumber)
        {
            var account = _accountRepository.GetByNumber(accountNumber);

            if (account == null)
                throw new ArgumentNullException("Wrong account number" + nameof(accountNumber));

            account.Deactivate();
        }
    }
}
