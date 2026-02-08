using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Application.Interfaces
{
    public interface IAccountOpeningService
    {
        Result OpenAdditionalSavingsAccount(string accountNumber);
        Result OpenAdditionalCurrentAccount(string accountNumber);
        Result OpenNewSavingsAccount(Customer customer);
        Result OpenNewCurrentAccount(Customer customer);
    }
}
