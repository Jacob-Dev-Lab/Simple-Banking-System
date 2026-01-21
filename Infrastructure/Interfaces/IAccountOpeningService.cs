using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Infrastructure.Interfaces
{
    public interface IAccountOpeningService
    {
        string OpenSavingsAccount(Guid customerID);
        string OpenCurrentAccount(Guid customerID);
    }
}
