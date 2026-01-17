using SimpleBankingSystem.Domain;

namespace SimpleBankingSystem.Interfaces
{
    internal interface ICustomerRepository
    {
        void Add(Customer customer);
        Customer GetById(Guid customerID);
        void Save(Customer customer);
    }
}
