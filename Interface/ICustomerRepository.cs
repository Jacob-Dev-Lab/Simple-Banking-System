using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Interface;

namespace SimpleBankingSystem.Repo
{
    internal interface ICustomerRepository
    {
        void Add(Customer customer);
        Customer? GetById(Guid customerID);
        void Save(Customer customer);
    }
}
