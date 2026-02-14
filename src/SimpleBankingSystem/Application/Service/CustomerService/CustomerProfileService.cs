using Microsoft.Extensions.Logging;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Application.Service.CustomerService
{
    public class CustomerProfileService(ICustomerRepository customerRepository, 
        IAccountRepository accountRepository, ILogger<CustomerProfileService> logger) : ICustomerProfileService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly ILogger<CustomerProfileService> _logger = logger;

        /* UpdateLastName, and UpdateEmailAddress methods first retrieve the account using the 
         * provided account number. They then retrieve the customer associated with the account 
         * and call the appropriate method on the customer to update the last name or email address. 
         * If the update operation fails due to a business rule violation, the method returns 
         * the failure result. If the update is successful, it saves the changes to the repository 
         * and returns a success result. */

        public Result UpdateLastName(string accountNumber, string lastname)
        {
            _logger.LogInformation("Initiated Lastname update on {accountNumber}", accountNumber);
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            if (account is null)
            {
                _logger.LogWarning("Failed to update Lastname: invalid account number");
                return Result.Failure(MessageAccountNotFound(accountNumber));
            }

            var customer = _customerRepository.GetCustomerById(account.CustomerID);

            var result = customer.ChangeLastname(lastname);

            if (result.IsFailure)
            {
                _logger.LogWarning("Failed to update Lastname: {message}", result.Message);
                return result;
            }

            _customerRepository.Save();
            _logger.LogInformation("Lastname updated successfully for {accountNumber}", accountNumber);

            return Result.Success("Lastname updated successfully");
        }
        public Result UpdateEmailAddress(string accountNumber, string emailAddress)
        {
            _logger.LogInformation("Initiated Email Address update on {accountNumber}", accountNumber);

            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            if (account is null)
            {
                _logger.LogWarning("Failed to update email address: invalid account number");
                return Result.Failure(MessageAccountNotFound(accountNumber));
            }

            var customer = _customerRepository.GetCustomerById(account.CustomerID);

            var result = customer.ChangeEmailAddress(emailAddress);

            if (result.IsFailure)
            {
                _logger.LogWarning("Failed to update email address: {message}", result.Message);
                return result;
            }

            _customerRepository.Save();
            _logger.LogInformation("Email Address updated successfully for {accountNumber}", accountNumber);

            return Result.Success("Email address updated successfully");
        }

        private static string MessageAccountNotFound(string accountNumber) => $"Account number {accountNumber} not found.";
    }
}
