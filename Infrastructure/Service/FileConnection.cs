using SimpleBankingSystem.Application.Interfaces;

namespace SimpleBankingSystem.Infrastructure.Service
{
    internal class FileConnection : IFileConnection
    {
        public (string customerPath, string accountPath, string transactionPath, string logPath) ConnectionString()
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var folderPath = Path.Combine(basePath, "BankManagementSystem");

            Directory.CreateDirectory(folderPath);

            string customerFile = Path.Combine(folderPath, "Customer.txt");
            string accountFile = Path.Combine(folderPath, "Account.txt");
            string transactionFile = Path.Combine(folderPath, "Transaction.txt");
            string logFile = Path.Combine(folderPath, "Log.txt");

            return (customerFile, accountFile, transactionFile, logFile);
        }
    }
}
