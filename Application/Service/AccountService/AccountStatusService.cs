using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Application.Service.AccountService
{
    internal class AccountStatusService(IAccountRepository accountRepository) : IAccountStatusService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        public Result ActivateAccount(string accountNumber)
        {
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            var ruleCheck = account.Activate();
            
            if (ruleCheck.IsFailure)
                return ruleCheck;

            _accountRepository.Save();

            return Result.Success();
        }

        public Result DeActivateAccount(string accountNumber)
        {
            var account = _accountRepository.GetAccountByAccountNumber(accountNumber);
            var ruleCheck = account.Deactivate();

            if (ruleCheck.IsFailure)
                return ruleCheck;

            _accountRepository.Save();

            return Result.Success();
        }
    }
}
