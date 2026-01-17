using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Interfaces;

namespace SimpleBankingSystem.Service.Account
{
    internal class AccountOperationService : IAccountOperationService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AccountOperationService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public void Deposit(string accountNumber, decimal amount)
        {
            var account = _accountRepository.GetByNumber(accountNumber);

            if (account == null)
                throw new ArgumentNullException("Wrong account number" + nameof(accountNumber));

            account.Deposit(amount);

            Transaction transaction = new(accountNumber, amount, "Deposit");
            _transactionRepository.Add(transaction);
        }

        public void Withdraw(string accountNumber, decimal amount)
        {
            var account = _accountRepository.GetByNumber(accountNumber);

            if (account == null)
                throw new ArgumentNullException("Wrong account number" + nameof(accountNumber));

            account.Withdraw(amount);

            Transaction transaction = new(accountNumber, amount, "Withrawal");
            _transactionRepository.Add(transaction);
        }
    }
}
