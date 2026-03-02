using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Infrastructure.Interface
{
    public interface IAccountRepository
    {
        void Add(Account account);
        Account? GetByNumber(string accountNumber);
        Account? GetById(Guid accountId);
        IReadOnlyCollection<Account> GetByCustomerId(Guid customerId);
        void Update(Account account);
    }
}
