using SimpleBankingSystem.Data;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Infrastructure.Interface;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    internal class EfTransactionRepo(BankDbContext context) : ITransactionRepository
    {
        private readonly BankDbContext _context = context;

        public void Add(Transaction transaction)
        {
            ArgumentNullException.ThrowIfNull(transaction, "Error: invalid transaction");

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public IEnumerable<Transaction> GetByAccountNumber(string accountNumber)
        {
            return _context.Transactions.Where(t => t.AccountNumber.Equals(accountNumber)).ToList();
        }

        public Transaction? GetById(Guid id)
        {
            return _context.Transactions.FirstOrDefault(t => t.Id.Equals(id));
        }
    }
}
