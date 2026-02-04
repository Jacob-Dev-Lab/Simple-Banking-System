using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Application.Interfaces
{
    public interface IAccountOperationService
    {
        Result Deposit(string accountNumber, decimal amount);
        Result Withdraw(string accountNumber, decimal amount);
    }
}
