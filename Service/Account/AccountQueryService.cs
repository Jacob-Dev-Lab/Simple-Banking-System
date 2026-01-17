using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Interfaces;
using SimpleBankingSystem.Utilities;

namespace SimpleBankingSystem.Service.Account
{
    internal class AccountQueryService : IAccountQueryService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AccountQueryService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public decimal GetAccountBalance(string accountNumber)
        {
            var account = _accountRepository.GetByNumber(accountNumber);

            if (account == null)
                throw new ArgumentNullException("Wrong accountnumber" + nameof(accountNumber));

            return account.Balance;
        }

        public IReadOnlyCollection<Transaction> GetTransactions(string accountNumber)
        {
            if (accountNumber == null)
                throw new ArgumentException("Wrong accountnumber" + nameof(accountNumber));

            return _transactionRepository.FindByAccountNumber(accountNumber);
        }

    }
}
