using SimpleBankingSystem.Domain;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Infrastructure.Interfaces;

namespace SimpleBankingSystem.Application.Service.Account
{
    internal class AccountOpeningService(IAccountRepository accountRepository,
        IGenerateAccountNumber generateAccountNumber) : IAccountOpeningService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly IGenerateAccountNumber _generateAccountNumber = generateAccountNumber;

        public string OpenSavingsAccount(Guid customerID)
        {
            EnsureCustomerDoesNotHaveAccountType(customerID, AccountType.Savings);

            string savingsAccountNumber = _generateAccountNumber.Generate();
            var account = new SavingsAccount(customerID, savingsAccountNumber);

            _accountRepository.Add(account);

            return savingsAccountNumber;
        }

        public string OpenCurrentAccount(Guid customerID)
        {
            EnsureCustomerDoesNotHaveAccountType(customerID, AccountType.Current);

            string currentAccountNumber = _generateAccountNumber.Generate();
            var account = new CurrentAccount(customerID, currentAccountNumber);

            _accountRepository.Add(account);

            return currentAccountNumber;
        }

        public void EnsureCustomerDoesNotHaveAccountType(Guid customerID, AccountType accountType)
        {
            var accounts = _accountRepository.GetAccountByAccountId(customerID);

            if (accounts.Any(x => x.AccountType == accountType))
                throw new InvalidOperationException($"Existing {accountType} Account found");
        }

    }
}
