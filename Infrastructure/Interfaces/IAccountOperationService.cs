using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Infrastructure.Interfaces
{
    internal interface IAccountOperationService
    {
        Result Deposit(string accountNumber, decimal amount);
        Result Withdraw(string accountNumber, decimal amount);
    }
}
