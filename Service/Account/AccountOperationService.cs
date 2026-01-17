using SimpleBankingSystem.Interfaces;
using SimpleBankingSystem.Domain;

namespace SimpleBankingSystem.Service.Account
{
    internal class AccountOperationService : IAccountOperationService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AccountOperationService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public void Deposit(string accountNumber, decimal amount)
        {
            var account = _accountRepository.GetByNumber(accountNumber);
            account.Deposit(amount);

            Transaction transaction = new(accountNumber, amount, "Deposit");
            _transactionRepository.Add(transaction);
            account.LinkTransaction(transaction.TransactionID);
        }

        public void Withdraw(string accountNumber, decimal amount)
        {
            var account = _accountRepository.GetByNumber(accountNumber);
            account.Withdraw(amount);

            Transaction transaction = new(accountNumber, amount, "Withrawal");
            _transactionRepository.Add(transaction);
            account.LinkTransaction(transaction.TransactionID);
        }
    }
}
