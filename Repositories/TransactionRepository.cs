using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Interfaces;
using SimpleBankingSystem.Utilities;

namespace SimpleBankingSystem.Repositories
{
    internal class TransactionRepository : AccountValidator, ITransactionRepository
    {
        private readonly List<Transaction> _transactions = [];
        public void Add(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            if (_transactions.Contains(transaction))
                throw new ArgumentException("Duplicate Transaction detected! Try again");

            _transactions.Add(transaction);
        }

        public IReadOnlyCollection<Transaction> FindByAccountNumber(string accountNumber)
        {
            ValidateAccountNumber(accountNumber);

            return _transactions.FindAll(t => t.AccountNumber == accountNumber);
        }

        public Transaction? FindById(Guid transactionID)
        {
            if (transactionID == Guid.Empty)
                throw new ArgumentNullException(nameof(_transactions));

            return _transactions.FirstOrDefault(t => t.TransactionID == transactionID);
        }

        public void Save(Transaction transaction)
        {
            //To be implemeted
        }
    }
}
