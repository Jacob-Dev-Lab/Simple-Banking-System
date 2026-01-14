using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Utilities
{
    abstract class AccountValidator
    {
        protected virtual void ValidateAccountNumber(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber) || string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Invalid Account Number: " + nameof(accountNumber));
        }
    }
}
