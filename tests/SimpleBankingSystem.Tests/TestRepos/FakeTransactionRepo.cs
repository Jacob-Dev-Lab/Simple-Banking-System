using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.Entities;

namespace TestSimpleBankingSystem.Tests.TestRepos
{
    public class FakeTransactionRepo : ITransactionRepository
    {
        private readonly List<Transaction> _transactions = [];
        public void Add(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public IReadOnlyCollection<Transaction> GetTransactionByAccountNumber(string accountNumber)
        {
            return _transactions.FindAll(t => t.AccountNumber.Equals(accountNumber));
        }

        public Transaction? GetTransactionByTransactionId(Guid transactionID)
        {
            return _transactions.FirstOrDefault(t => t.TransactionID.Equals(transactionID));
        }

        public List<Transaction> Load()
        {
            return _transactions;
        }

        public void Save()
        {
            //throw new NotImplementedException();
        }
    }
}
