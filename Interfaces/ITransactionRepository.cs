using SimpleBankingSystem.Domain;

namespace SimpleBankingSystem.Interfaces
{
    internal interface ITransactionRepository
    {
        void Add(Transaction transaction);
        IReadOnlyCollection<Transaction> FindByAccountNumber(string accountNumber);
        Transaction? FindById(Guid transactionID);

        void Save(Transaction transaction);
    }
}
