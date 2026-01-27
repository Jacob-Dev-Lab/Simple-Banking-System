using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Infrastructure.Interfaces
{
    internal interface ICustomerProfileService
    {
        Result UpdateLastName(Guid customerID, string lastname);
        Result UpdateEmailAddress(Guid customerID, string emailAddress);
    }
}
