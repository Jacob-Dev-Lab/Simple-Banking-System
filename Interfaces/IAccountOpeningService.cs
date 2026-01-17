namespace SimpleBankingSystem.Interfaces
{
    public interface IAccountOpeningService
    {
        string OpenSavingsAccount(Guid customerID);
        string OpenCurrentAccount(Guid customerID);
    }
}
