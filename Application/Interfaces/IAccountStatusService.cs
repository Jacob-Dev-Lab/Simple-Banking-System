using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Application.Interfaces
{
    internal interface IAccountStatusService
    {
        Result ActivateAccount(string accountNumber);
        Result DeActivateAccount(string accountNumber);
    }
}
