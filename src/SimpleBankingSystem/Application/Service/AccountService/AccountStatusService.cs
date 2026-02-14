using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Application.Service.AccountService
{
    public class AccountStatusService(IAccountRepository accountRepository) : IAccountStatusService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        /* The ActivateAccount and DeActivateAccount methods first retrieve the account
         * using the provided account number. They then call the Activate or Deactivate method
         * on the account, which checks the business rules for the operation. If the operation fails
         * due to a business rule violation, the method returns the failure result.
         * If the operation is successful, it saves the changes to the repository and returns a success result.*/
        public Result ActivateAccount(string accountNumber)
        {
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            var ruleCheck = account.Activate();
            
            if (ruleCheck.IsFailure)
                return ruleCheck;

            _accountRepository.Save();

            return Result.Success("Account activated successfully.");
        }

        public Result DeActivateAccount(string accountNumber)
        {
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            var ruleCheck = account.Deactivate();

            if (ruleCheck.IsFailure)
                return ruleCheck;

            _accountRepository.Save();

            return Result.Success("Account deactivated successfully.");
        }
    }
}
