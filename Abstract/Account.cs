abstract class Account (Guid customerID, string accountNumber)
{
    public Guid CustomerID { get; } = customerID;
    public string AccountNumber {get;} = accountNumber;
    public decimal Balance {get; protected set;} = 0m;
    public string? Type { get; }
    public DateOnly DateCreated {get;} = DateOnly.FromDateTime(DateTime.Now);
    public bool IsDeactivated {get; protected set;} = false;

    private readonly List<Transaction> _transactions = [];
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

    public virtual void Deposit(decimal amount)
    {
        EnsureAccountIsActive();
        ValidateAmount(amount);
        Balance += amount;
    }

    public abstract void Withdraw(decimal amount);

    protected void ValidateAmount(decimal amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Amount must be greater than Zero(0)");
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