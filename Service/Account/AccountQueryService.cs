using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Interface;
using SimpleBankingSystem.Utilities;

namespace SimpleBankingSystem.Service.Account
{
    internal class AccountQueryService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountQueryService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
            var account = _accountRepository.GetByNumber(accountNumber);
            
            if (account == null)
                throw new ArgumentNullException("Wrong accountnumber" + nameof(accountNumber));

            return account.Transactions; 
        }

    }
}
