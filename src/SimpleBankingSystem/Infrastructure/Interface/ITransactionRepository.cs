using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Infrastructure.Interface
{
    public interface ITransactionRepository
    {
        void Add(Transaction transaction);
        Transaction? GetById(Guid id);
        IEnumerable<Transaction> GetByAccountNumber(string accountNumber);
    }
}
