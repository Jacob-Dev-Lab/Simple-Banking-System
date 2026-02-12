using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Domain
{
    public sealed class SavingsAccount(Guid customerID, string accountNumber, decimal balance = 0m) : 
        Account(customerID, accountNumber, AccountType.Savings, balance)
    {
        private static readonly decimal _minimumBalance = 50m;

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

            if (Balance - amount < _minimumBalance)
                return Result.Failure("Violates minimum balance."); ;

            Balance -= amount;
            return Result.Success();
        }
    }
}