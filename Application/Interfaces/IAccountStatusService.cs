using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Application.Interfaces
{
    public interface IAccountStatusService
    {
        Result ActivateAccount(string accountNumber);
        Result DeActivateAccount(string accountNumber);
    }
}
