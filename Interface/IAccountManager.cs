using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Interface
{
    internal interface IAccountManager
    {
        void AddCustomer(Customer customer);
        Customer? GetCustomer(Guid customerID);
        void AddAccount(Account account);
        Account? GetAccount(string accountNumber);
    }
}
