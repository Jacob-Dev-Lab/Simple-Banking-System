using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Domain
{
    sealed class CurrentAccount(Guid customerID, string accountNumber) : Account(customerID, accountNumber, AccountType.Current)
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