using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Interface;

namespace SimpleBankingSystem.Service.Account
{
    internal class AccountOperationService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountOperationService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Deposit(string accountNumber, decimal amount)
        {
            var account = _accountRepository.GetByNumber(accountNumber);

            if (account == null)
                throw new ArgumentNullException("Wrong account number" + nameof(accountNumber));

            account.Deposit(amount);
        }

        public void Withdraw(string accountNumber, decimal amount)
        {
            var account = _accountRepository.GetByNumber(accountNumber);

            if (account == null)
                throw new ArgumentNullException("Wrong account number" + nameof(accountNumber));

            account.Withdraw(amount);
        }
    }
}
