using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Domain
{
    public class SavingsAccount : Account
    {
        public SavingsAccount(Guid customerId, string accountNumber, decimal balance = 0m)
            : base(customerId, accountNumber, balance) { }

        protected SavingsAccount() : base() { } // EF constructor

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