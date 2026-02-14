using Microsoft.Extensions.Logging;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.ErrorHandler;
using SimpleBankingSystem.Domain.Interfaces;

namespace SimpleBankingSystem.Application.Service.AccountService
{
    public class AccountOpeningService(IAccountRepository accountRepository,
        ICustomerRepository customerRepository,
        IGenerateAccountNumber generateAccountNumber,
        ILogger<AccountOpeningService> logger) : IAccountOpeningService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly IGenerateAccountNumber _generateAccountNumber = generateAccountNumber;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly ILogger<AccountOpeningService> _logger = logger;

        /* This method is responsible for processing the account opening logic,
         * including creating the account, linking it to the customer,
         * and saving the changes to the repositories.*/
        private string AccountOpenningProcessor(Guid customerId, Customer customer, AccountType accountType)
        {
            Account account;

            string accountNumber = _generateAccountNumber.Generate();

            if (accountType.Equals(AccountType.Savings))
                account = new SavingsAccount(customerId, accountNumber);
            else
                account = new CurrentAccount(customerId, accountNumber);

            _accountRepository.Add(account);
            customer.LinkAccountNumber(accountNumber);

            _accountRepository.Save();
            _customerRepository.Save();

            return accountNumber;
        }

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

            var customerId = _accountRepository.GetAccountByAccountNumber(accountNumber).CustomerID;
            var customer = _customerRepository.GetCustomerById(customerId);

            var savingsAccountNumber = AccountOpenningProcessor(customerId, customer, AccountType.Savings);

            return Result.Success("Your savings account number is: " + savingsAccountNumber);
        }

        public Result OpenAdditionalCurrentAccount(string accountNumber)
        {
            var ruleCheck = EnsureCustomerDoesNotHaveAccountType(accountNumber, AccountType.Current);
            if (ruleCheck.IsFailure)
                return ruleCheck;

            var customerId = _accountRepository.GetAccountByAccountNumber(accountNumber).CustomerID;
            var customer = _customerRepository.GetCustomerById(customerId);

            var currentAccountNumber = AccountOpenningProcessor(customerId, customer, AccountType.Current);

            return Result.Success("Your current account number is: " + currentAccountNumber);
        }

        /* The OpenNewSavingsAccount and OpenNewCurrentAccount methods are responsible 
         * for opening new accounts for customers who do not have any existing accounts. 
         * They first add the customer to the customer repository, then call the 
         * AccountOpenningProcessor method to create the new account and link it to the customer. 
         * Finally, they return a success result with the new account number.*/
        public Result OpenNewSavingsAccount(Customer customer)
        {
            _logger.LogInformation("Initiated account opening for customer: {CustomerId}", customer.CustomerId);
            _customerRepository.Add(customer);

            var savingsAccountNumber = AccountOpenningProcessor(customer.CustomerId, customer, AccountType.Savings);

            _logger.LogInformation("Successfully opened savings account for customer: {CustomerId}", customer.CustomerId);

            return Result.Success("Your savings account number is: " + savingsAccountNumber);
        }

        public Result OpenNewCurrentAccount(Customer customer)
        {
            _logger.LogInformation("Initiated account opening for customer: {CustomerId}", customer.CustomerId);
            _customerRepository.Add(customer);

            var currentAccountNumber = AccountOpenningProcessor(customer.CustomerId, customer, AccountType.Current);

            _logger.LogInformation("Successfully opened current account for customer: {CustomerId}", customer.CustomerId);

            return Result.Success("Your current account number is: " + currentAccountNumber);
        }

        /* The EnsureCustomerDoesNotHaveAccountType method checks if the customer associated 
         * with the given account number already has an account of the specified type. 
         * It retrieves the customer's accounts and checks if any of them match the specified account type. 
         * If a matching account is found, it returns a failure result with an appropriate message. 
         * If no matching account is found, it returns a success result.*/
        public Result EnsureCustomerDoesNotHaveAccountType(string accountNumber, AccountType accountType)
        {
            var customerID = _accountRepository.GetAccountByAccountNumber(accountNumber).CustomerID;
            var accounts = _accountRepository.GetAccountByAccountId(customerID);

            if (accounts.Any(x => x.AccountType == accountType))
                return Result.Failure($"Existing {accountType} Account found");

            return Result.Success();
        }
    }
}
