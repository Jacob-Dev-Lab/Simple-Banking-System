using SimpleBankingSystem.Domain.Enums;

namespace SimpleBankingSystem.Domain
{
    sealed class CurrentAccount(Guid customerID, string accountNumber) : Account(customerID, accountNumber, AccountType.Current)
    {
        public override void Withdraw(decimal amount)
        {
            EnsureAccountIsActive();
            ValidateAmount(amount);

            if (amount > Balance)
                throw new InvalidOperationException("Insufficient Balance");

            Balance -= amount;
        }
    }
}