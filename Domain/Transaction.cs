internal class Transaction (string accountNumber, decimal amount, string transactionType)
{
    public Guid TransactionID { get; } = new Guid();
    public string AccountNumber { get; } = accountNumber;
    public decimal Amount { get; } = amount;
    public string TransactionType { get; } = transactionType;
    public DateOnly TransactionDate { get; } = DateOnly.FromDateTime(DateTime.Now);
}