namespace SimpleBankingSystem.Interfaces
{
    internal interface IAccountStatusService
    {
        void ActivateAccount(string accountNumber);
        void DeActivateAccount(string accountNumber);
    }
}
