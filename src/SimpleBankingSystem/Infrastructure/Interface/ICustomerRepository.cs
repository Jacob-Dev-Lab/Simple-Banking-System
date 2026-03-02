using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Infrastructure.Interface
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        Customer? GetById(Guid id);
        void Update(Customer customer);
    }
}
