namespace SimpleBankingSystem.Infrastructure.Interfaces
{
    internal interface ICustomerProfileService
    {
        void UpdateLastName(Guid customerID, string lastname);
        void UpdateEmailAddress(Guid customerID, string emailAddress);
    }
}
