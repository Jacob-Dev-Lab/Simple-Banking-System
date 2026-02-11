using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using SimpleBankingSystem.Application.Service;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Domain.Entities
{
    public sealed class Customer
    {
        public Customer(string lastName, string otherNames, DateOnly dateOfBirth, string email)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name required", nameof(lastName));

            if (string.IsNullOrWhiteSpace(otherNames))
                throw new ArgumentException("Othernames required", nameof(otherNames));

            if (dateOfBirth.Equals(null))
                throw new ArgumentException("Date of birth is required", nameof(dateOfBirth));

            if (dateOfBirth > DateOnly.FromDateTime(DateTime.Now.AddYears(-18)))
                throw new ArgumentException("Customer must be at least 18 years old to open an account", nameof(dateOfBirth));

            if (dateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new ArgumentException("Date of birth cannot be in the future", nameof(dateOfBirth));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Valid email required", nameof(email));

            EmailValidator.Validate(email);

            CustomerId = Guid.NewGuid();
            LastName = lastName;
            OtherNames = otherNames;
            DateOfBirth = dateOfBirth;
            Email = email;
        }
        public Guid CustomerId { get; }
        public string LastName { get; private set; }
        public string OtherNames { get; private set; }
        public DateOnly DateOfBirth { get; }
        public string Email { get; private set; }

        private readonly List<string> _accountNumbers = [];
        public IReadOnlyCollection<string> AccountNumbers => _accountNumbers.AsReadOnly();

        public void LinkAccountNumber(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber) || string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Unable to link account, try again.");

            _accountNumbers.Add(accountNumber);
        }

        public Result ChangeLastname(string newLastName)
        {
            if (string.IsNullOrWhiteSpace(newLastName))
                return Result.Failure("A valid Last name is required.");

            LastName = newLastName;
            return Result.Success();
        }
        public Result ChangeEmailAddress(string newEmailAddress)
        {
            if (!new MailAddress(newEmailAddress).Address.Equals(newEmailAddress))
                return Result.Failure("A valid email address is required.");

            Email = newEmailAddress;
            return Result.Success();
        }
    }
}