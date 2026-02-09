using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Application.Interfaces
{
    public interface IAccountQueryService
    {
        decimal GetAccountBalance(string accountNumber);
        IReadOnlyCollection<Transaction> GetTransactions(string accountNumber);
    }
}
