using SimpleBankingSystem.Interfaces;
using SimpleBankingSystem.Utilities;
using SimpleBankingSystem.Domain;

namespace SimpleBankingSystem.Repositories
{
    class AccountRepository : AccountValidator, IAccountRepository
    {
        private static readonly List<Account> _accounts = [];
        public void Add(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (_accounts.Contains(account))
                throw new InvalidOperationException("Account already exists.");

            _accounts.Add(account);
        }

        public Account GetByNumber(string accountNumber)
        {
            ValidateAccountNumber(accountNumber);

            return _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber) ??
                throw new KeyNotFoundException("Account number does not exist/invalid");
        }

        public IReadOnlyCollection<Account> GetById(Guid customerID)
        {
            return _accounts.FindAll(x => x.CustomerID == customerID);
        }

        public void Save(Account account)
        {
            //To Be Implemented in the Future For Persistence
        }
    }
}
