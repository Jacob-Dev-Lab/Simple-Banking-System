using SimpleBankingSystem.Data;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Infrastructure.Interface;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    public class EfCustomerRepo(BankDbContext context) : ICustomerRepository
    {
        private readonly BankDbContext _context = context;

        public void Add(Customer customer)
        {
            ArgumentNullException.ThrowIfNull(customer, "Error: invalid customer");

            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public Customer? GetById(Guid id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id.Equals(id));
        }

        public void Update(Customer customer)
        {
            ArgumentNullException.ThrowIfNull(customer, "Error: invalid customer");

            _context.Customers.Update(customer);
            _context.SaveChanges();
        }
    }
}
