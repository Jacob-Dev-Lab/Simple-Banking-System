using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Infrastructure.Interfaces
{
    internal interface ITransactionRepository
    {
        void Add(Transaction transaction);
        IReadOnlyCollection<Transaction> GetTransactionByAccountNumber(string accountNumber);
        Transaction? GetTransactionByTransactionId(Guid transactionID);
        void Save();
        List<Transaction> Load();
    }
}
