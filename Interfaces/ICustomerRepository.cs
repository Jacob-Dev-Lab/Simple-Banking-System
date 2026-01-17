using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Interfaces
{
    internal interface ICustomerRepository
    {
        void Add(Customer customer);
        Customer? GetById(Guid customerID);
        void Save(Customer customer);
    }
}
