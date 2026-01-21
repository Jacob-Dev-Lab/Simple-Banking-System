using SimpleBankingSystem.Domain.Enums;

namespace SimpleBankingSystem.Domain
{
    abstract class Account(Guid customerID, string accountNumber, AccountType accountType)
    {
        public Guid CustomerID { get; } = customerID;
        public string AccountNumber { get; } = accountNumber;
        public AccountType AccountType { get; } = accountType;
        public decimal Balance { get; protected set; } = 0m;
        public DateOnly DateCreated { get; } = DateOnly.FromDateTime(DateTime.Now);
        public bool IsDeactivated { get; protected set; } = false;

        private readonly List<Guid> _transactionsID = [];
        public IReadOnlyCollection<Guid> TransactionsID => _transactionsID.AsReadOnly();

        public void LinkTransaction(Guid transactionID)
        {
            if (transactionID == Guid.Empty)
                throw new ArgumentNullException("Invalid transaction id");

            _transactionsID.Add(transactionID);
        }

        public virtual void Deposit(decimal amount)
        {
            EnsureAccountIsActive();
            ValidateAmount(amount);
            Balance += amount;
        }

        public abstract void Withdraw(decimal amount);

        protected void ValidateAmount(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException("Amount must be greater than zero (0).");
        }

        public void Activate()
        {
            if (!IsDeactivated)
                return;

            IsDeactivated = false;
        }
        public void Deactivate()
        {
            if (IsDeactivated)
                return;

            IsDeactivated = true;
        }

        protected void EnsureAccountIsActive()
        {
            if (IsDeactivated == true)
                throw new InvalidOperationException("Account is Deactivated");
        }
    }
}