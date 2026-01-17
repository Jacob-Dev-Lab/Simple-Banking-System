using SimpleBankingSystem.Interfaces;
using SimpleBankingSystem.Domain;

namespace SimpleBankingSystem.Service.Account
{
    internal class AccountQueryService : IAccountQueryService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AccountQueryService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public decimal GetAccountBalance(string accountNumber)
        {
            var account = _accountRepository.GetByNumber(accountNumber);
            return account.Balance;
        }

        public IReadOnlyCollection<Transaction> GetTransactions(string accountNumber)
        {
            return _transactionRepository.FindByAccountNumber(accountNumber);
        }

    }
}
