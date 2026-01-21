namespace SimpleBankingSystem.Infrastructure.Interfaces
{
    internal interface IDataStorageConnection
    {
        (string customerPath, string accountPath, string transactionPath) ConnectionString();
    }
}
