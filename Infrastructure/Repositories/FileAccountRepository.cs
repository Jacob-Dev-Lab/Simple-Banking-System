using System.Text.Json;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain;
using SimpleBankingSystem.Domain.Enums;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    internal class FileAccountRepository(string filePath, ILogger logger) : IAccountRepository
    {
        private readonly string _filePath = filePath;
        private readonly ILogger _logger = logger;
        private readonly List<Account> _accounts = [];
        private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

        public void Add(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("Error: invalid account, try again");

            if (_accounts.Contains(account))
                throw new InvalidOperationException("Account already exists.");

            _accounts.Add(account);
            Save();
        }

        public IReadOnlyCollection<Account> GetAccountByAccountId(Guid customerGuid)
        {
            return _accounts.FindAll(a => a.CustomerID == customerGuid);
        }

        public Account GetAccountByAccountNumber(string accountNumber)
        {
            return _accounts.FirstOrDefault
                (a => a.AccountNumber == accountNumber) ??
                throw new KeyNotFoundException
                    ("Account does not exist");
        }

        public void Save()
        {
            try
            {
                var serializedAccountData = JsonSerializer.Serialize(_accounts, _jsonOptions);
                File.WriteAllText(_filePath, serializedAccountData);
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error saving account data: {ex.Message}");
                throw;
            }
        }

        public List<Account> Load()
        {
            try
            {
                if (!File.Exists(_filePath))
                    File.WriteAllText(_filePath, "[]");

                var jsonData = File.ReadAllText(_filePath);
                var rawJsonData = JsonSerializer.Deserialize<List<JsonElement>>(jsonData) ?? [];

                List<Account> deserializedAccounts = [];

                foreach (var jsonElement in rawJsonData)
                {
                    var accountType = (AccountType)jsonElement.GetProperty("AccountType").GetInt32();

                    Account deserializedData = accountType switch
                    {
                        AccountType.Savings => JsonSerializer.Deserialize<SavingsAccount>(jsonElement.GetRawText())!,
                        AccountType.Current => JsonSerializer.Deserialize<CurrentAccount>(jsonElement.GetRawText())!,
                        _ => throw new InvalidOperationException("Unknown account type.")
                    };

                    deserializedAccounts.Add(deserializedData);
                }
                _accounts.Clear();
                _accounts.AddRange(deserializedAccounts);
                return _accounts;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error loading account data: {ex.Message}");
                throw;
            }
        }
    }
}
