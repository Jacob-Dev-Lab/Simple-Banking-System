using System.Net.Mail;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.ErrorHandler;
using SimpleBankingSystem.Presentation;

namespace SimpleBankingSystem.Application.Service.CustomerService
{
    internal class CustomerProfileService(ICustomerRepository customerRepository, 
        IAccountRepository accountRepository) : ICustomerProfileService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IAccountRepository _accountRepository = accountRepository;

        /* UpdateLastName, and UpdateEmailAddress methods first retrieve the account using the 
         * provided account number. They then retrieve the customer associated with the account 
         * and call the appropriate method on the customer to update the last name or email address. 
         * If the update operation fails due to a business rule violation, the method returns 
         * the failure result. If the update is successful, it saves the changes to the repository 
         * and returns a success result. */

        public Result UpdateLastName(string accountNumber, string lastname)
        {
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            if (account is null)
                return Result.Failure("Account not found.");

            var customer = _customerRepository.GetCustomerById(account.CustomerID);

            var result = customer.ChangeLastname(lastname);

            if (result.IsFailure) 
                return result;

            _customerRepository.Save();
            return Result.Success();
        }
        public Result UpdateEmailAddress(string accountNumber, string emailAddress)
        {
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            if (account is null)
                return Result.Failure("Account not found.");

            var customer = _customerRepository.GetCustomerById(account.CustomerID);

            var result = customer.ChangeEmailAddress(emailAddress);

            if (result.IsFailure) 
                return result;

            _customerRepository.Save();
            return Result.Success();
        }
    }
}
