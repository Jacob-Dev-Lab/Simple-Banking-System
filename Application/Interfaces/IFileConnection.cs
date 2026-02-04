namespace SimpleBankingSystem.Application.Interfaces
{
    internal interface IFileConnection
    {
        (string customerPath, string accountPath, string transactionPath, string logPath) ConnectionString();
    }
}
