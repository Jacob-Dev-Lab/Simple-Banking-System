using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Application.Interfaces
{
    public interface IAccountOpeningService
    {
        Result OpenSavingsAccount(Guid customerID);
        Result OpenCurrentAccount(Guid customerID);
        Result OpenSavingsAccount(Customer customer);
        Result OpenCurrentAccount(Customer customer);
    }
}
