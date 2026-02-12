namespace SimpleBankingSystem.Application.Service
{
    public abstract class AccountValidator
    {
        /* This method validates the account number. It checks if the account number
         * is null, empty, or consists only of whitespace characters. If any of these
         * conditions are true, it throws an ArgumentException with a message indicating
         * that the account number is invalid.*/

        protected virtual void ValidateAccountNumber(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber) || string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Invalid Account Number: " + nameof(accountNumber));
        }
    }
}
