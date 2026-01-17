using SimpleBankingSystem.Domain;

namespace SimpleBankingSystem.Interfaces
{
    internal interface IAccountRepository
    {
        void Add(Account account);
        Account GetByNumber(string accountNumber);
        IReadOnlyCollection<Account> GetById(Guid accountGuid);
        void Save(Account account);
    }
}
