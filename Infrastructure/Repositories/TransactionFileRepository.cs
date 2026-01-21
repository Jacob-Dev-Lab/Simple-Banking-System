using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Infrastructure.Interfaces;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    internal class TransactionFileRepository : ITransactionRepository
    {
        private readonly string _filePath;
        private readonly List<Transaction> _transactions = [];
        private readonly JsonSerializerOptions _jsonOption = new() { WriteIndented = true};

        public TransactionFileRepository(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(Transaction transaction)
        {
            if (transaction == null) 
                throw new ArgumentNullException(nameof(transaction));

            if (_transactions.Contains(transaction))
                throw new InvalidOperationException("Duplicate Transaction");

            _transactions.Add(transaction);
            Save();
        }

        public IReadOnlyCollection<Transaction> GetTransactionByAccountNumber(string accountNumber)
        {
            return _transactions.FindAll(t => t.AccountNumber == accountNumber) ??
                throw new KeyNotFoundException("Invalid account number.");
        }

        public Transaction GetTransactionByTransactionId(Guid transactionID)
        {
            return _transactions.FirstOrDefault(t => t.TransactionID == transactionID) ??
                throw new KeyNotFoundException("Invalid transaction ID.");
        }

        public void Save()
        {
            var jsonData = JsonSerializer.Serialize(_transactions, _jsonOption);
            File.WriteAllText(_filePath, jsonData);
        }

        public List<Transaction> Load()
        {
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");

            var jsonData = File.ReadAllText(_filePath);
            var deserialisedData = JsonSerializer.Deserialize<List<Transaction>>(jsonData) ?? [];

            _transactions.Clear();
            _transactions.AddRange(deserialisedData);

            return _transactions;
        }
    }
}
