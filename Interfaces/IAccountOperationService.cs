using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Interfaces
{
    internal interface IAccountOperationService
    {
        void Deposit(string accountNumber, decimal amount);
        void Withdraw(string accountNumber, decimal amount);
    }
}
