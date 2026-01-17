using SimpleBankingSystem.Interfaces;
using SimpleBankingSystem.Domain;

namespace SimpleBankingSystem.Service.Account
{
    internal class AccountOpeningService : IAccountOpeningService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IGenerateAccountNumber _generateAccountNumber;

        public AccountOpeningService(IGenerateAccountNumber generateAccountNumber, IAccountRepository accountRepository)
        {
            _generateAccountNumber = generateAccountNumber;
            _accountRepository = accountRepository;
        }

        public string OpenSavingsAccount(Guid customerID)
        {
            string type = "SavingsAccount";
            HasAccountType(customerID, type);

            string savingsAccountNumber = _generateAccountNumber.Generate();
            var account = new SavingsAccount(customerID, savingsAccountNumber);
            _accountRepository.Add(account);

            return savingsAccountNumber;
        }

        public string OpenCurrentAccount(Guid customerID)
        {
            string type = "CurrentAccount";
            HasAccountType(customerID, type);

            string currentAccountNumber = _generateAccountNumber.Generate();
            var account = new CurrentAccount(customerID, currentAccountNumber);
            _accountRepository.Add(account);

            return currentAccountNumber;
        }

        public void HasAccountType(Guid customerID, string type)
        {
            var accounts = _accountRepository.GetById(customerID);

            if (accounts.Count > 0 && accounts.Any(x => x.GetType().ToString().Equals(type)))
                throw new InvalidOperationException("Existing Savings Account Found");
        }

    }
}
