using System.Text.Json;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Infrastructure.Interfaces;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    internal class CustomerFileRepository(string filePath) : ICustomerRepository
    {
        private readonly string _filePath = filePath;
        private readonly List<Customer> _customers = [];
        private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

        public void Add(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("Invalid customer", nameof(customer));

            var existingCustomer = _customers.FirstOrDefault
                (c =>
                    c.DateOfBirth == customer.DateOfBirth &&
                    c.Email == customer.Email
                );

            if (existingCustomer != null)
                throw new InvalidOperationException("Customer already exists.");

            _customers.Add(customer);
            Save();
        }

        public Customer GetCustomerById(Guid customerID)
        {
            return _customers.FirstOrDefault
                (c => c.CustomerId == customerID) ??
                throw new KeyNotFoundException
                    ("Customer does not exist/incorrect customer information");
        }

        public void Save()
        {
            var serializedustomerData = JsonSerializer.Serialize(_customers, _jsonOptions);
            File.WriteAllText(_filePath, serializedustomerData);
        }

        public List<Customer> Load()
        {
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");

            var jsonData = File.ReadAllText(_filePath);
            var deserialisedData = JsonSerializer.Deserialize<List<Customer>>(jsonData) ?? [];

            _customers.Clear();
            _customers.AddRange(deserialisedData);
            return _customers;
        }
    }
}
