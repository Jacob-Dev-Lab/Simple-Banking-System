using SimpleBankingSystem.Infrastructure.Interface;

namespace SimpleBankingSystem.Infrastructure.Service
{
    public class FileConnection : IFileConnection
    {
        private readonly string _folderPath;

        /* The constructor of the FileConnection class initializes the folder path where 
         * the files will be stored. It retrieves the path to the Application Data folder 
         * using Environment.GetFolderPath and combines it with a subfolder named "BankManagementSystem". 
         * The Directory.CreateDirectory method is then called to ensure that the directory exists, 
         * creating it if it does not. */

        public FileConnection()
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _folderPath = Path.Combine(basePath, "BankManagementSystem");

            Directory.CreateDirectory(_folderPath);
        }

        // The following properties return the full file paths for the Customer, Account, Transaction, and Log files.
        public string CustomerFilePath => Path.Combine(_folderPath, "Customer.txt");
        public string AccountFilePath => Path.Combine(_folderPath, "Account.txt");
        public string TransactionFilePath => Path.Combine(_folderPath, "Transaction.txt");
        public string LogFilePath => Path.Combine(_folderPath, "Log.txt");
    }
}
