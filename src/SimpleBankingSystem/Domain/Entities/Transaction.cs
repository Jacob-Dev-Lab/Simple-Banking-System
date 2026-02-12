using SimpleBankingSystem.Domain.Enums;

namespace SimpleBankingSystem.Domain.Entities
{
    public class Transaction(string accountNumber, decimal amount, TransactionType transactionType)
    {
        public Guid TransactionID { get; } = Guid.NewGuid();
        public string AccountNumber { get; } = accountNumber;
        public decimal Amount { get; } = amount;
        public TransactionType TransactionType { get; } = transactionType;
        public DateOnly TransactionDate { get; } = DateOnly.FromDateTime(DateTime.Now);

        public override string ToString()
        {
            return $"{TransactionDate} - {TransactionID} | {TransactionType} | {Amount}";
        }
    }
}