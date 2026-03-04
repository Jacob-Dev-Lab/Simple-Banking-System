using Microsoft.Extensions.Logging;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.ErrorHandler;
using SimpleBankingSystem.Domain.Interfaces;
using SimpleBankingSystem.Infrastructure.Interface;

namespace SimpleBankingSystem.Application.Service.AccountService
{
    public class AccountOpeningService(IAccountRepository accountRepository,
        ICustomerRepository customerRepository,
        IGenerateAccountNumber generateAccountNumber,
        ILogger<AccountOpeningService> logger) : IAccountOpeningService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IGenerateAccountNumber _generateAccountNumber = generateAccountNumber;
        private readonly ILogger<AccountOpeningService> _logger = logger;

        /* The OpenAdditionalSavingsAccount and OpenAdditionalCurrentAccount methods 
         * first check if the customer already has an account of the specified type 
         * using the EnsureCustomerDoesNotHaveAccountType method. If the check fails, 
         * they return a failure result. If the check passes, they retrieve the customer's 
         * information and call the AccountOpenningProcessor method to create the new 
         * account and link it to the customer. Finally, they return a success result 
         * with the new account number.*/
        public Result OpenAdditionalSavingsAccount(string accountNumber)
        {
            var ruleCheck = EnsureCustomerDoesNotHaveAccountType(accountNumber, AccountType.Savings);

            if (ruleCheck.IsFailure)
                return ruleCheck;

            var account = _accountRepository.GetByNumber(accountNumber);
            if (account is null)
                return Result.Failure("Account not found.");

            var customerId = account.CustomerId;

            var result = AccountOpenningProcessor(customerId, AccountType.Savings);
            if (result.IsFailure)
                return result;

            return Result.Success("Your savings account number is: " + result.Message);
        }

        public Result OpenAdditionalCurrentAccount(string accountNumber)
        {
            var ruleCheck = EnsureCustomerDoesNotHaveAccountType(accountNumber, AccountType.Current);
            if (ruleCheck.IsFailure)
                return ruleCheck;

            var account = _accountRepository.GetByNumber(accountNumber);
            if (account is null)
                return Result.Failure("Account not Found");

            var customerId = account.CustomerId;

            var result = AccountOpenningProcessor(customerId, AccountType.Current);

            return Result.Success("Your current account number is: " + result.Message);
        }

        /* The OpenNewSavingsAccount and OpenNewCurrentAccount methods are responsible 
         * for opening new accounts for customers who do not have any existing accounts. 
         * They first add the customer to the customer repository, then call the 
         * AccountOpenningProcessor method to create the new account and link it to the customer. 
         * Finally, they return a success result with the new account number.*/
        public Result OpenNewSavingsAccount(Customer customer)
        {
            if (customer is null)
                return Result.Failure("Invalid customer, try again.");

            _logger.LogInformation("Initiated account opening for customer: {CustomerId}", customer.Id);
            _customerRepository.Add(customer);

            var result = AccountOpenningProcessor(customer.Id, AccountType.Savings);

            _logger.LogInformation("Successfully opened savings account for customer: {CustomerId}", customer.Id);

            return Result.Success("Your savings account number is: " + result.Message);
        }

        public Result OpenNewCurrentAccount(Customer customer)
        {
            if (customer is null)
                return Result.Failure("Invalid customer, try again.");

            _logger.LogInformation("Initiated account opening for customer: {CustomerId}", customer.Id);

            _customerRepository.Add(customer);

            var currentAccountNumber = AccountOpenningProcessor(customer.Id, AccountType.Current);

            _logger.LogInformation("Successfully opened current account for customer: {CustomerId}", customer.Id);

            return Result.Success("Your current account number is: " + currentAccountNumber);
        }

        /* The EnsureCustomerDoesNotHaveAccountType method checks if the customer associated 
         * with the given account number already has an account of the specified type. 
         * It retrieves the customer's accounts and checks if any of them match the specified account type. 
         * If a matching account is found, it returns a failure result with an appropriate message. 
         * If no matching account is found, it returns a success result.*/
        public Result EnsureCustomerDoesNotHaveAccountType(string accountNumber, AccountType accountType)
        {
            if (string.IsNullOrEmpty(accountNumber))
                return Result.Failure("Error: Enter a valid account number.");

            var account = _accountRepository.GetByNumber(accountNumber);
            if (account is null)
                return Result.Failure("Account not Found, enter a valid account number.");

            var accounts = _accountRepository.GetByCustomerId(account.CustomerId);

            var requestedType = accountType switch
            {
                AccountType.Current => typeof(CurrentAccount),
                AccountType.Savings => typeof(SavingsAccount),
                _ => throw new ArgumentOutOfRangeException(nameof(accountType)),
            };

            if (accounts.Any(a => a.GetType() == requestedType))
                return Result.Failure($"Existing {accountType} Account found");

            return Result.Success();
        }

        /* This method is responsible for processing the account opening logic,
         * including creating the account, linking it to the customer,
         * and saving the changes to the repositories.*/
        private Result AccountOpenningProcessor(Guid customerId, AccountType accountType)
        {
            if (customerId == Guid.Empty)
                return Result.Failure("Customer ID cannot be empty");

            string accountNumber = _generateAccountNumber.Generate();

            Account? account = accountType switch
            {
                AccountType.Current => new CurrentAccount(customerId, accountNumber),
                AccountType.Savings => new SavingsAccount(customerId, accountNumber),
                _ => null
            };

            if (account is null)
                return Result.Failure("Invalid account type, enter a valid account type.");

            _accountRepository.Add(account);

            return Result.Success(accountNumber);
        }
    }
}
