using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Domain
{
    public sealed class CurrentAccount(Guid customerID, string accountNumber, decimal balance = 0m) : 
        Account(customerID, accountNumber, AccountType.Current, balance)
    {
        public override Result Withdraw(decimal amount)
        {
            var activeCheck = EnsureAccountIsActive();
            if (activeCheck.IsFailure)
                return activeCheck;

            var amountCheck = ValidateAmount(amount);
            if (amountCheck.IsFailure)
                return amountCheck;

            if (amount > Balance)
                return Result.Failure("Insufficient Funds.");

            Balance -= amount;
            return Result.Success();
        }
    }
}