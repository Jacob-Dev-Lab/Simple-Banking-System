namespace SimpleBankingSystem.Interfaces
{
    internal interface IAccountOperationService
    {
        void Deposit(string accountNumber, decimal amount);
        void Withdraw(string accountNumber, decimal amount);
    }
}
