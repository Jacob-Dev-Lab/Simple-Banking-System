using SimpleBankingSystem.Domain;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.ErrorHandler;
using SimpleBankingSystem.Infrastructure.Interfaces;

namespace SimpleBankingSystem.Application.Service.AccountService
{
    internal class AccountOpeningService(IAccountRepository accountRepository, 
        ICustomerRepository customerRepository,
        IGenerateAccountNumber generateAccountNumber) : IAccountOpeningService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly IGenerateAccountNumber _generateAccountNumber = generateAccountNumber;
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public Result OpenSavingsAccount(Guid customerID)
        {
            var ruleCheck = EnsureCustomerDoesNotHaveAccountType(customerID, AccountType.Savings);

            if (ruleCheck.IsFailure)
                return ruleCheck;

            string savingsAccountNumber = _generateAccountNumber.Generate();
            var account = new SavingsAccount(customerID, savingsAccountNumber);

            _accountRepository.Add(account);

            return Result.Success(savingsAccountNumber);
        }

        public Result OpenSavingsAccount(Customer customer)
        {
            _customerRepository.Add(customer);

            var result = OpenSavingsAccount(customer.CustomerId);

            if (result.IsFailure) 
                return result;

            customer.LinkAccountNumber(result.Message);
            _customerRepository.Save();

            return Result.Success(result.Message);
        }

        public Result OpenCurrentAccount(Guid customerID)
        {
            var ruleCheck = EnsureCustomerDoesNotHaveAccountType(customerID, AccountType.Current);
            if (ruleCheck.IsFailure)
                return ruleCheck;

            string currentAccountNumber = _generateAccountNumber.Generate();
            var account = new CurrentAccount(customerID, currentAccountNumber);

            _accountRepository.Add(account);

            return Result.Success(currentAccountNumber);
        }

        public Result OpenCurrentAccount(Customer customer)
        {
            _customerRepository.Add(customer);

            var result = OpenCurrentAccount(customer.CustomerId);

            if (result.IsFailure) 
                return result;

            customer.LinkAccountNumber(result.Message);
            _customerRepository.Save();

            return Result.Success(result.Message);
        }

        public Result EnsureCustomerDoesNotHaveAccountType(Guid customerID, AccountType accountType)
        {
            var accounts = _accountRepository.GetAccountByAccountId(customerID);

            if (accounts.Any(x => x.AccountType == accountType))
                return Result.Failure($"Existing {accountType} Account found");

            return Result.Success();
        }
    }
}
