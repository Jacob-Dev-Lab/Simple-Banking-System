using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
