using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Domain.Entities
{
    public abstract class Account
    {
        public int Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public string AccountNumber { get; private set; }
        public decimal Balance { get; protected set; }
        public DateOnly DateCreated { get; private set; }
        public bool IsActive { get; protected set; }

        protected Account(Guid customerId, string accountNumber, decimal balance = 0m)
        {
            if (customerId.Equals(Guid.Empty))
                throw new ArgumentException("Customer ID cannot be empty.", nameof(customerId));

            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Account number is required.", nameof(accountNumber));

            CustomerId = customerId;
            AccountNumber = accountNumber;
            Balance = balance;
            DateCreated = DateOnly.FromDateTime(DateTime.Now);
            IsActive = true;
        }

        protected Account() { } //EF Core Constructor

        //Today
        // private readonly List<Guid> _transactionsID = [];
        // public IReadOnlyCollection<Guid> TransactionsID => _transactionsID.AsReadOnly();

        //Today
        // public void LinkTransaction(Guid transactionID)
        // {
        //     if (transactionID.Equals(Guid.Empty))
        //         throw new ArgumentException("Transaction ID cannot be empty.", nameof(transactionID));

        //     _transactionsID.Add(transactionID);
        // }

        public virtual Result Deposit(decimal amount)
        {
            var activeCheck = EnsureAccountIsActive();
            if (activeCheck.IsFailure)
                return activeCheck;

            var amountCheck = ValidateAmount(amount);
            if (amountCheck.IsFailure)
                return amountCheck;

            Balance += amount;
            return Result.Success();
        }

        public abstract Result Withdraw(decimal amount);

        protected Result ValidateAmount(decimal amount)
        {
            if (amount <= 0)
                return Result.Failure("Amount must be greater than Zero.");

            return Result.Success();
        }

        public Result Activate()
        {
            if (IsActive)
                return Result.Failure("Account is already activated.");

            IsActive = true;
            return Result.Success();
        }
        public Result Deactivate()
        {
            if (!IsActive)
                return Result.Failure("Account is already deactivated.");

            IsActive = false;
            return Result.Success();
        }

        protected Result EnsureAccountIsActive()
        {
            if (!IsActive)
                return Result.Failure("Transaction canceled: account is deactivated.");

            return Result.Success();
        }
    }
}