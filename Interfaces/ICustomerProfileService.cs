namespace SimpleBankingSystem.Interfaces
{
    internal interface ICustomerProfileService
    {
        void UpdateLastName(Guid customerID, string lastname);
        void UpdateEmailAddress(Guid customerID, string emailAddress);
    }
}
