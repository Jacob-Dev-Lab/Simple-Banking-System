using SimpleBankingSystem.Utilities;
using SimpleBankingSystem.Domain;
using SimpleBankingSystem.Infrastructure.Interfaces;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    class AccountRepository : AccountValidator, IAccountRepository
    {
        private static readonly List<Account> _accounts = [];
        public void Add(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("Error: invalid account, try again");

            if (_accounts.Contains(account))
                throw new InvalidOperationException("Account already exists.");

            _accounts.Add(account);
        }

        public Account GetAccountByAccountNumber(string accountNumber)
        {
           return _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber) ??
                throw new KeyNotFoundException("Account number does not exist/invalid");
        }

        public IReadOnlyCollection<Account> GetAccountByAccountId(Guid customerID)
        {
            return _accounts.FindAll(x => x.CustomerID == customerID);
        }

        public void Save()
        {
            //To Be Implemented in the Future For Persistence
        }

        public List<Account> Load()
        {
            //To Be Implemented in the Future For Persistence
            return [];
        }
    }
}
