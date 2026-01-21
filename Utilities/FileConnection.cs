using SimpleBankingSystem.Infrastructure.Interfaces;

namespace SimpleBankingSystem.Utilities
{
    internal class FileConnection : IDataStorageConnection
    {
        public (string customerPath, string accountPath, string transactionPath) ConnectionString()
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var folderPath = Path.Combine(basePath, "BankManagementSystem");

            Directory.CreateDirectory(folderPath);

            string customerFile = Path.Combine(folderPath, "Customer.txt");
            string accountFile = Path.Combine(folderPath, "Account.txt");
            string transactionFile = Path.Combine(folderPath, "Transaction.txt");

            return (customerFile, accountFile, transactionFile);
        }
    }
}
