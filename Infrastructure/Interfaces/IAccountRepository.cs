using SimpleBankingSystem.Domain;

namespace SimpleBankingSystem.Infrastructure.Interfaces
{
    internal interface IAccountRepository
    {
        void Add(Account account);
        Account GetAccountByAccountNumber(string accountNumber);
        IReadOnlyCollection<Account> GetAccountByAccountId(Guid accountGuid);
        void Save();
        List<Account> Load();
    }
}
