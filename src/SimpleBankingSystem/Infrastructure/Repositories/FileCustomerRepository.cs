using System.Text.Json;
using Microsoft.Extensions.Logging;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Infrastructure.Interface;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    internal class FileCustomerRepository(IFileConnection connection, 
        ILogger<FileCustomerRepository> logger) : ICustomerRepository
    {
        private readonly string _filePath = connection.CustomerFilePath;
        private readonly ILogger<FileCustomerRepository> _logger = logger;

        private readonly List<Customer> _customers = [];
        private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

        /*  The Add method adds a new customer to the repository. It checks if the provided 
         *  customer is null and throws an ArgumentNullException if it is. It also checks 
         *  if a customer with the same date of birth and email already exists in the 
         *  repository and throws an InvalidOperationException if it does. If the customer 
         *  is valid and does not already exist, it adds the customer to the list of 
         *  customers and saves the updated list to the file. */
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

        /*  The GetCustomerById method retrieves a customer from the repository based on 
         *  their unique identifier (customerID). It searches through the list of customers 
         *  and returns the first customer that matches the provided ID. If no customer is 
         *  found with the given ID, it throws a KeyNotFoundException with a message indicating 
         *  that the customer does not exist or that the provided information is incorrect. */
        public Customer GetCustomerById(Guid customerID)
        {
            return _customers.FirstOrDefault
                (c => c.CustomerId == customerID) ??
                throw new KeyNotFoundException
                    ("Customer does not exist/incorrect customer information");
        }

        /*  The Save method is responsible for saving the current list of customers to a file. 
         *  It serializes the list of customers into JSON format using the JsonSerializer and 
         *  writes the serialized data to the specified file path. If any exceptions occur 
         *  during the saving process, it logs the error message using the provided logger and 
         *  rethrows the exception to be handled by the calling code. */
        public void Save()
        {
            try
            {
                var serializedustomerData = JsonSerializer.Serialize(_customers, _jsonOptions);
                File.WriteAllText(_filePath, serializedustomerData);

                //_logger.LogInformation("Customer data saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving customer data: {ex.Message}");
                throw;
            }
        }

        /*  The Load method is responsible for loading the list of customers from a file. It checks 
         *  if the specified file exists, and if it does not, it creates an empty file with an 
         *  empty JSON array. It then reads the contents of the file, deserializes the JSON data 
         *  into a list of customers, and updates the internal list of customers with the loaded 
         *  data. If any exceptions occur during the loading process, it logs the error message 
         *  using the provided logger and rethrows the exception to be handled by the calling code. */
        public List<Customer> Load()
        {
            try
            {
                if (!File.Exists(_filePath))
                    File.WriteAllText(_filePath, "[]");

                var jsonData = File.ReadAllText(_filePath);
                var deserialisedData = JsonSerializer.Deserialize<List<Customer>>(jsonData) ?? [];

                _customers.Clear();
                _customers.AddRange(deserialisedData);
                //_logger.LogInformation("Customer data loaded successfully.");

                return _customers;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading customer data: {ex.Message}");
                throw;
            }
        }
    }
}
