using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Application.Interfaces
{
    public interface IAccountQueryService
    {
        Result GetAccountBalance(string accountNumber);
        IReadOnlyCollection<Transaction> GetTransactions(string accountNumber);
    }
}
