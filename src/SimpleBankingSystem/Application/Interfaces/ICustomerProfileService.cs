using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Application.Interfaces
{
    public interface ICustomerProfileService
    {
        Result UpdateLastName(string accountNumber, string lastname);
        Result UpdateEmailAddress(string accountNumber, string emailAddress);
    }
}
