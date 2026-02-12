using System;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain;

namespace TestSimpleBankingSystem.Tests.TestRepos
{
    public class FakeAccountRepo : IAccountRepository
    {
        private readonly List<Account> _accounts = [];

        public void Add(Account account)
        {
            _accounts.Add(account);
        }

        public IReadOnlyCollection<Account> GetAccountByAccountId(Guid accountGuid)
        {
            return _accounts.FindAll(a => a.CustomerID.Equals(accountGuid));
        }

        public Account GetAccountByAccountNumber(string accountNumber)
        {
            return _accounts.FirstOrDefault(a => a.AccountNumber.Equals(accountNumber));
        }

        public List<Account> Load()
        {
            return _accounts;
        }

        public void Save()
        {
            //throw new NotImplementedException();
        }
    }
}
