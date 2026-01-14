using SimpleBankingSystem.Interface;
using SimpleBankingSystem.Repo;

namespace SimpleBankingSystem.Service.Account
{
    internal class AccountOpeningService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IGenerateAccountNumber _generateAccountNumber;

        public AccountOpeningService(ICustomerRepository customerRepository,
            IGenerateAccountNumber generateAccountNumber, IAccountRepository accountRepository)
        {
            _customerRepository = customerRepository;
            _generateAccountNumber = generateAccountNumber;
            _accountRepository = accountRepository;
        }

        public void OpenSavingsAccount(Guid customerID, string accountNumber)
        {
            HasAccountType(customerID, "Savings");

            var account = new SavingsAccount(customerID, accountNumber);
            _accountRepository.Add(account);
        }

        public void OpenCurrentAccount(Guid customerID, string accountNumber)
        {
            HasAccountType(customerID, "Current");

            var account = new CurrentAccount(customerID, accountNumber);
            _accountRepository.Add(account);
        }

        public void HasAccountType(Guid customerID, string type)
        {
            var accounts = _accountRepository.GetById(customerID);

            if (accounts.Count > 0 && accounts.Any(x => x.Type == type))
                throw new InvalidOperationException("Existing Savings Account Found");
        }

    }
}
