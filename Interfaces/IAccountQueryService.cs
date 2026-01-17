using SimpleBankingSystem.Domain;

namespace SimpleBankingSystem.Interfaces
{
    internal interface IAccountQueryService
    {
        decimal GetAccountBalance(string accountNumber);
        IReadOnlyCollection<Transaction> GetTransactions(string accountNumber);
    }
}
