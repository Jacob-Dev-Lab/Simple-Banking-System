using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Application.Service.CustomerService
{
    public class CreateCustomerService : ICreateCustomerService
    {
        public Customer CreateCustomer(string firstName, string lastName, DateOnly dateOfBirth, string email) 
        { 
            return new Customer(firstName, lastName, dateOfBirth, email); 
        }
    }
}
