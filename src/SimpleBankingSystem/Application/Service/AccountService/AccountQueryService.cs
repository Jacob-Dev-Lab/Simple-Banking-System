using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Application.Service.AccountService
{
    internal class AccountQueryService(IAccountRepository accountRepository, 
        ITransactionRepository transactionRepository) : IAccountQueryService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly ITransactionRepository _transactionRepository = transactionRepository;

        // The GetAccountBalance method retrieves the account using the
        // provided account number and returns its balance.
        public Result GetAccountBalance(string accountNumber)
        {
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            return Result.Success("Your account balance = " + account.Balance);
        }

        /* The GetTransactions method retrieves the transactions associated with 
         * the specified account number and returns them as a read-only collection.*/
        public IReadOnlyCollection<Transaction> GetTransactions(string accountNumber)
        {
            return _transactionRepository.GetTransactionByAccountNumber(accountNumber);
        }

    }
}
