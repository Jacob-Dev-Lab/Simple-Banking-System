using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Interfaces
{
    public interface IAccountOpeningService
    {
        string OpenSavingsAccount(Guid customerID);
        string OpenCurrentAccount(Guid customerID);
    }
}
