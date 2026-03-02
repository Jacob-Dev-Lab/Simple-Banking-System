using SimpleBankingSystem.Data;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Infrastructure.Interface;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    internal class EfAccountRepo(BankDbContext context) : IAccountRepository
    {
        private readonly BankDbContext _context = context;

        public void Add(Account account) 
        { 
            ArgumentNullException.ThrowIfNull(account, nameof(account));

            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public Account? GetById(Guid id)
        {
            if (id.Equals(Guid.Empty))
                throw new ArgumentException(nameof(id), "Error: invalid id.");

            return _context.Accounts.FirstOrDefault(a => a.CustomerId.Equals(id));
        }

        public Account? GetByNumber(string accountNumber)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(accountNumber, nameof(accountNumber));

            return _context.Accounts.FirstOrDefault(a => a.AccountNumber.Equals(accountNumber));
        }

        public IReadOnlyCollection<Account> GetByCustomerId(Guid id)
        {
            if (id.Equals(Guid.Empty))
                throw new ArgumentException(nameof(id), "Error: invalid id.");

            return _context.Accounts.Where(a => a.CustomerId.Equals(id)).ToList();
        }

        public void Update(Account account)
        {
            ArgumentNullException.ThrowIfNull(account, "Error: invalid account.");

            _context.Accounts.Update(account);
            _context.SaveChanges();
        }
    }
}
