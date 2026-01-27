using System.ComponentModel.DataAnnotations;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Domain.Entities
{
    public sealed class Customer
    {
        public  Customer(string lastName, string otherNames, DateOnly dateOfBirth, string email)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name required", nameof(lastName));

            if (string.IsNullOrWhiteSpace(otherNames))
                throw new ArgumentException("Othernames required", nameof(otherNames));

            if (dateOfBirth.Equals(new DateOnly()))
                throw new ArgumentException("Date of birth required", nameof(dateOfBirth));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email address required", nameof(email));

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
            if (accountNumber == null)
                throw new ArgumentNullException("Unable to link account, try again.");

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
            if (string.IsNullOrWhiteSpace(newEmailAddress))
                return Result.Failure("A valid email address is required.");

            Email = newEmailAddress;
            return Result.Success();
        }
    }
}