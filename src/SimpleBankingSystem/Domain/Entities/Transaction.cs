using SimpleBankingSystem.Domain.Enums;

namespace SimpleBankingSystem.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public int AccountId { get; private set; }
        public string AccountNumber { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public DateOnly TimeStamp { get; private set; }

        public Transaction(string accountNumber, decimal amount, TransactionType transactionType)
        {
            Id = Guid.NewGuid();
            AccountNumber = accountNumber;
            Amount = amount;
            Type = transactionType;
            TimeStamp = DateOnly.FromDateTime(DateTime.UtcNow);
        }

        public Transaction(){} //EF Core Constructor

        public override string ToString()
        {
            return $"{TimeStamp} - {Id} | {Type} | {Amount}";
        }
    }
}