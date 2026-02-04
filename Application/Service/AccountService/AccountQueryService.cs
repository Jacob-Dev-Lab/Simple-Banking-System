using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.ErrorHandler;
using SimpleBankingSystem.Infrastructure.Repositories;

namespace SimpleBankingSystem.Application.Service.AccountService
{
    internal class AccountQueryService(IAccountRepository accountRepository, 
        ITransactionRepository transactionRepository) : IAccountQueryService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly ITransactionRepository _transactionRepository = transactionRepository;

        public decimal GetAccountBalance(string accountNumber)
        {
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            return account.Balance;
        }

        public IReadOnlyCollection<Transaction> GetTransactions(string accountNumber)
        {
            return _transactionRepository.GetTransactionByAccountNumber(accountNumber);
        }

    }
}
