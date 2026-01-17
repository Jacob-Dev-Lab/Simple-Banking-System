using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Interfaces
{
    internal interface IAccountStatusService
    {
        void ActivateAccount(string accountNumber);
        void DeActivateAccount(string accountNumber);
    }
}
