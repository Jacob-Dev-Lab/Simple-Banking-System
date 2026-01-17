using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Interfaces
{
    internal interface ICustomerProfileService
    {
        void UpdateLastName(Guid customerID, string lastname);
        void UpdateEmailAddress(Guid customerID, string emailAddress);
    }
}
