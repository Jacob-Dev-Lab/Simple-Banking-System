using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Infrastructure.Interfaces;
using SimpleBankingSystem.Infrastructure.Repositories;

namespace SimpleBankingSystem.Application.Service.Account
{
    internal class AccountOperationService(IAccountRepository accountRepository, 
        ITransactionRepository transactionRepository) : IAccountOperationService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly ITransactionRepository _transactionRepository = transactionRepository;

        public void Deposit(string accountNumber, decimal amount)
        {
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            account.Deposit(amount);

            Transaction transaction = new(accountNumber, amount, TransactionType.Deposit);
            _transactionRepository.Add(transaction);

            account.LinkTransaction(transaction.TransactionID);

            _accountRepository.Save();
        }

        public void Withdraw(string accountNumber, decimal amount)
        {
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            account.Withdraw(amount);

            Transaction transaction = new(accountNumber, amount, TransactionType.Withdrawal);
            _transactionRepository.Add(transaction);

            account.LinkTransaction(transaction.TransactionID);

            _accountRepository.Save();
        }
    }
}
