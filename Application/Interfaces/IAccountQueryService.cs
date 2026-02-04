using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Application.Interfaces
{
    internal interface IAccountQueryService
    {
        decimal GetAccountBalance(string accountNumber);
        IReadOnlyCollection<Transaction> GetTransactions(string accountNumber);
    }
}
