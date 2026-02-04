using System.Text.Json;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    internal class FileTransactionRepository(string filePath, ILogger logger) : ITransactionRepository
    {
        private readonly string _filePath = filePath;
        private readonly ILogger _logger = logger;
        private readonly List<Transaction> _transactions = [];
        private readonly JsonSerializerOptions _jsonOption = new() { WriteIndented = true};

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
            try
            {
                var jsonData = JsonSerializer.Serialize(_transactions, _jsonOption);
                File.WriteAllText(_filePath, jsonData);
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error saving transaction data: {ex.Message}");
                throw;
            }
        }

        public List<Transaction> Load()
        {
            try
            {
                if (!File.Exists(_filePath))
                    File.WriteAllText(_filePath, "[]");

                var jsonData = File.ReadAllText(_filePath);
                var deserialisedData = JsonSerializer.Deserialize<List<Transaction>>(jsonData) ?? [];

                _transactions.Clear();
                _transactions.AddRange(deserialisedData);

                return _transactions;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error loading transaction data: {ex.Message}");
                throw;
            }
        }
    }
}
