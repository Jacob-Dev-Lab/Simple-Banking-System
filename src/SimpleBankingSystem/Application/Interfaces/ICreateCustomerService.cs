using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Application.Interfaces
{
    public interface ICreateCustomerService
    {
        Customer CreateCustomer(string firstName, string lastName, DateOnly dateOfBirth, string email);
    }
}
