using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Infrastructure.Interfaces
{
    internal interface IAccountStatusService
    {
        Result ActivateAccount(string accountNumber);
        Result DeActivateAccount(string accountNumber);
    }
}
