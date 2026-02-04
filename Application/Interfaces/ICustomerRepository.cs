using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Application.Interfaces
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        Customer GetCustomerById(Guid customerID);
        void Save();
        List<Customer> Load();
    }
}
