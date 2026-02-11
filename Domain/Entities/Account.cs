using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Domain
{
    public abstract class Account
    {
        protected Account(Guid customerID, string accountNumber, AccountType accountType, decimal balance = 0m)
        {
            if (customerID.Equals(Guid.Empty))
                throw new ArgumentException("Customer ID cannot be empty.", nameof(customerID));

            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Account number is required.", nameof(accountNumber));

            if (!Enum.IsDefined(accountType))
                throw new ArgumentOutOfRangeException(nameof(accountType));

            CustomerID = customerID;
            AccountNumber = accountNumber;
            AccountType = accountType;

            Balance = balance;
            DateCreated = DateOnly.FromDateTime(DateTime.Now);
            IsActive = true;
        }

        //public Account() { }

        public Guid CustomerID { get; }
        public string AccountNumber { get; }
        public AccountType AccountType { get; }
        public decimal Balance { get; protected set; }
        public DateOnly DateCreated { get; }
        public bool IsActive { get; protected set; }

        private readonly List<Guid> _transactionsID = [];
        public IReadOnlyCollection<Guid> TransactionsID => _transactionsID.AsReadOnly();

        public void LinkTransaction(Guid transactionID)
        {
            if (transactionID.Equals(Guid.Empty))
                throw new ArgumentException("Transaction ID cannot be empty.", nameof(transactionID));

            _transactionsID.Add(transactionID);
        }

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