using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Domain
{
    public class CurrentAccount : Account
    {
        public CurrentAccount(Guid customerId, string accountNumber, decimal balance = 0m)
            : base(customerId, accountNumber, balance) { }

        protected CurrentAccount() : base() { } // EF constructor

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