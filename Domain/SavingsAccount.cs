namespace SimpleBankingSystem.Domain
{
    sealed class SavingsAccount(Guid customerID, string accountNumber) : Account(customerID, accountNumber)
    {
        public string? Type { get; } = "Savings";

        private static readonly decimal _minimumBalance = 50m;

        public override void Withdraw(decimal amount)
        {
            EnsureAccountIsActive();
            ValidateAmount(amount);

            if (amount > Balance)
                throw new InvalidOperationException("Insufficient Balance");

            if (Balance - amount < _minimumBalance)
                throw new InvalidOperationException("Violates Minimum Balance");

            Balance -= amount;
        }
    }
}