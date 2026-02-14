using Microsoft.Extensions.Logging;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Application.Service.AccountService
{
    public class AccountOperationService(IAccountRepository accountRepository, 
        ITransactionRepository transactionRepository,
        ILogger<AccountOperationService> logger) : IAccountOperationService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly ITransactionRepository _transactionRepository = transactionRepository;
        private readonly ILogger<AccountOperationService> _logger = logger;

        /* The Deposit and Withdraw methods first retrieve the account using the provided account number. 
         * They then call the Deposit or Withdraw method on the account, which checks the business rules 
         * for the operation. If the rule check fails, they return a failure result. If the rule check passes, 
         * they create a new Transaction object with the appropriate details and add it to the 
         * transaction repository. They also link the transaction to the account and save the changes to 
         * the account repository. Finally, they return a success result. */

        public Result Deposit(string accountNumber, decimal amount)
        {
            _logger.LogInformation("Deposit Initiated on {AccountNumber}", accountNumber);
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            
            var ruleCheck = account.Deposit(amount);
            if (ruleCheck.IsFailure)
            {
                _logger.LogWarning("Deposit transaction failed for account {AccountNumber}: {Reason}",
                    accountNumber, ruleCheck.Message);
                return ruleCheck;
            }

            Transaction transaction = new(accountNumber, amount, TransactionType.Deposit);
            _transactionRepository.Add(transaction);

            account.LinkTransaction(transaction.TransactionID);

            _accountRepository.Save();
            _logger.LogInformation("Deposit transaction completed successfully");

            return Result.Success($"£{amount}: Deposited successsfully");
        }

        public Result Withdraw(string accountNumber, decimal amount)
        {
            _logger.LogInformation("Withdrawal Initiated on {accountNumber}", accountNumber);
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            
            var ruleCheck = account.Withdraw(amount);
            if (ruleCheck.IsFailure)
            {
                _logger.LogWarning("Withdrawal transaction failed for account {AccountNumber}: {Reason}",
                    accountNumber, ruleCheck.Message);
                return ruleCheck;
            }

            Transaction transaction = new(accountNumber, amount, TransactionType.Withdrawal);
            _transactionRepository.Add(transaction);

            account.LinkTransaction(transaction.TransactionID);

            _accountRepository.Save();
            _logger.LogInformation("Withdrawal transaction completed successfully");

            return Result.Success($"£{amount}: Withdrawn successsfully");
        }
    }
}
