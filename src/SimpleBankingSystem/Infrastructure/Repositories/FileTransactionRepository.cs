using System.Text.Json;
using Microsoft.Extensions.Logging;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Infrastructure.Interface;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    internal class FileTransactionRepository(IFileConnection connection, 
        ILogger<FileTransactionRepository> logger) : ITransactionRepository
    {
        private readonly string _filePath = connection.TransactionFilePath;
        private readonly ILogger<FileTransactionRepository> _logger = logger;

        private readonly List<Transaction> _transactions = [];
        private readonly JsonSerializerOptions _jsonOption = new() { WriteIndented = true};

        /*  The Add method adds a new transaction to the repository. It checks if the provided 
         *  transaction is null and throws an ArgumentNullException if it is. It also checks 
         *  if the transaction already exists in the repository and throws an InvalidOperationException 
         *  if it does. If the transaction is valid and does not already exist, it adds the 
         *  transaction to the list of transactions and saves the updated list to the file. */
        public void Add(Transaction transaction)
        {
            if (transaction == null) 
                throw new ArgumentNullException(nameof(transaction));

            if (_transactions.Contains(transaction))
                throw new InvalidOperationException("Duplicate Transaction");

            _transactions.Add(transaction);
            Save();
        }

        /*  The GetTransactionByAccountNumber method retrieves all transactions associated 
         *  with a specific account number. It uses the FindAll method to filter the transactions 
         *  based on the provided account number. If no transactions are found for the given 
         *  account number, it throws a KeyNotFoundException. */
        public IReadOnlyCollection<Transaction> GetTransactionByAccountNumber(string accountNumber)
        {
            return _transactions.FindAll(t => t.AccountNumber == accountNumber) ??
                throw new KeyNotFoundException("Invalid account number.");
        }

        /*  The GetTransactionByTransactionId method retrieves a transaction based on its unique 
         *  transaction ID. It uses the FirstOrDefault method to find the transaction with the 
         *  specified transaction ID. If no transaction is found with the given ID, it throws a 
         *  KeyNotFoundException. */
        public Transaction GetTransactionByTransactionId(Guid transactionID)
        {
            return _transactions.FirstOrDefault(t => t.TransactionID == transactionID) ??
                throw new KeyNotFoundException("Invalid transaction ID.");
        }

        /*  The Save method is responsible for saving the current list of transactions to a file. 
         *  It serializes the list of transactions into JSON format using the JsonSerializer and 
         *  writes the JSON data to the specified file path. If any exceptions occur during the 
         *  saving process, it logs the error message and rethrows the exception. */
        public void Save()
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(_transactions, _jsonOption);
                File.WriteAllText(_filePath, jsonData);

                //_logger.LogInformation("Transaction data saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving transaction data: {ex.Message}");
                throw;
            }
        }

        /*  The Load method is responsible for loading the list of transactions from a file. It checks 
         *  if the file exists and creates an empty file if it does not. It then reads the JSON data 
         *  from the file, deserializes it into a list of transactions, and updates the internal list 
         *  of transactions. If any exceptions occur during the loading process, it logs the error 
         *  message and rethrows the exception. Finally, it returns the list of transactions. */
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
                //_logger.LogInformation("Transaction data loaded successfully.");

                return _transactions;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading transaction data: {ex.Message}");
                throw;
            }
        }
    }
}
