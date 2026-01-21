using SimpleBankingSystem.Utilities;
using SimpleBankingSystem.Infrastructure.Interfaces;
using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    internal class TransactionRepository : AccountValidator, ITransactionRepository
    {
        private readonly List<Transaction> _transactions = [];
        public void Add(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction) + "Example");

            if (_transactions.Contains(transaction))
                throw new InvalidOperationException("Transaction already exists.");

            _transactions.Add(transaction);
        }

        public IReadOnlyCollection<Transaction> GetTransactionByAccountNumber(string accountNumber)
        {
            ValidateAccountNumber(accountNumber);

            return _transactions.FindAll(t => t.AccountNumber == accountNumber);
        }

        public Transaction? GetTransactionByTransactionId(Guid transactionID)
        {
            if (transactionID == Guid.Empty)
                throw new ArgumentNullException(nameof(_transactions));

            return _transactions.FirstOrDefault(t => t.TransactionID == transactionID);
        }

        public void Save()
        {
            //To be implemeted
        }

        public List<Transaction> Load()
        {
            //To be implemeted
            return [];
        }
    }
}
