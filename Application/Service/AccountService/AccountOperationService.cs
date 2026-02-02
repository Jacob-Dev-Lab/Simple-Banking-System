using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.ErrorHandler;
using SimpleBankingSystem.Infrastructure.Interfaces;

namespace SimpleBankingSystem.Application.Service.AccountService
{
    public class AccountOperationService(IAccountRepository accountRepository, 
        ITransactionRepository transactionRepository) : IAccountOperationService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly ITransactionRepository _transactionRepository = transactionRepository;

        public Result Deposit(string accountNumber, decimal amount)
        {
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            
            var ruleCheck = account.Deposit(amount);
            if (ruleCheck.IsFailure)
                return ruleCheck;

            Transaction transaction = new(accountNumber, amount, TransactionType.Deposit);
            _transactionRepository.Add(transaction);

            account.LinkTransaction(transaction.TransactionID);

            _accountRepository.Save();

            return Result.Success();
        }

        public Result Withdraw(string accountNumber, decimal amount)
        {
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            
            var ruleCheck = account.Withdraw(amount);
            if (ruleCheck.IsFailure)
                return ruleCheck;

            Transaction transaction = new(accountNumber, amount, TransactionType.Withdrawal);
            _transactionRepository.Add(transaction);

            account.LinkTransaction(transaction.TransactionID);

            _accountRepository.Save();

            return Result.Success();
        }
    }
}
