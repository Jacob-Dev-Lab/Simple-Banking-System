using System.Text.Json;
using Microsoft.Extensions.Logging;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Infrastructure.Interface;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    internal class FileAccountRepository(IFileConnection connection, ILogger<FileAccountRepository> logger) : IAccountRepository
    {
        private readonly string _filePath = connection.AccountFilePath;
        private readonly ILogger<FileAccountRepository> _logger = logger;

        private readonly List<Account> _accounts = [];
        private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

        /*  The Add method adds a new account to the repository. It checks if the provided 
         *  account is null and throws an ArgumentNullException if it is. It also checks 
         *  if the account already exists in the repository and throws an InvalidOperationException 
         *  if it does. If the account is valid and does not already exist, it adds the 
         *  account to the list of accounts and saves the updated list to the file. */
        public void Add(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("Error: invalid account, try again");

            if (_accounts.Contains(account))
                throw new InvalidOperationException("Account already exists.");

            _accounts.Add(account);
            Save();
        }

        /*  The GetAccountByAccountId method retrieves a collection of accounts associated with a 
         *  specific customer ID. It filters the list of accounts based on the provided 
         *  customer GUID and returns the matching accounts as a read-only collection. */
        public IReadOnlyCollection<Account> GetAccountByAccountId(Guid customerGuid)
        {
            return _accounts.FindAll(a => a.CustomerID == customerGuid);
        }

        /*  The GetAccountByAccountNumber method retrieves an account based on the provided 
         *  account number. It searches through the list of accounts and returns the matching 
         *  account if found. If no account is found with the specified account number, it 
         *  throws a KeyNotFoundException to indicate that the account does not exist. */
        public Account GetAccountByAccountNumber(string accountNumber)
        {
            return _accounts.FirstOrDefault
                (a => a.AccountNumber == accountNumber) ??
                throw new KeyNotFoundException
                    ("Account does not exist");
        }

        /*  The Save method serializes the list of accounts into JSON format and writes it 
         *  to a file. It handles any exceptions that may occur during the saving process 
         *  by logging the error and rethrowing the exception. */
        public void Save()
        {
            try
            {
                var serializedAccountData = JsonSerializer.Serialize(_accounts, _jsonOptions);
                File.WriteAllText(_filePath, serializedAccountData);

                //_logger.LogInformation($"Saved accounts to file successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving account data: {ex.Message}");
                throw;
            }
        }

        /*  The Load method reads account data from a JSON file, deserializes it 
         *  into Account objects, and returns a list of accounts. It handles cases where 
         *  the file does not exist by creating an empty file and ensures that any errors 
         *  during loading are logged and propagated appropriately. */
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
                //_logger.LogInformation($"Accounts loaded successfully.");

                return _accounts;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading account data: {ex.Message}");
                throw;
            }
        }
    }
}
