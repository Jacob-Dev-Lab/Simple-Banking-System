using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Interface
{
    internal interface IAccountRepository
    {
        void Add(Account account);
        Account? GetByNumber(string accountNumber);
        IReadOnlyCollection<Account> GetById(Guid accountGuid);
        void Save(Account account);
    }
}
