using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Application.Service.AccountService;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Utilities;

namespace SimpleBankingSystem.Presentation
{
    internal static class IOHelper
    {
        // This method collects customer information from user input and returns it as a tuple.
        public static (string lastName, string otherNames, DateOnly dateOfBirth, string email) CustomerInformation()
        {
            string lastName = UserInputOutput.GetUserInputString("Kindly enter Lastname: ");
            string otherNames = UserInputOutput.GetUserInputString("Kindly enter other names: ");
            DateOnly dateOfBirth = UserInputOutput.GetUserDateOfBirth("Kindly enter date of birth (YYYY-MM-DD): ");
            string email = UserInputOutput.GetUserEmailString("Kindly enter email address (example@gmail.com): ");

            return (lastName, otherNames, dateOfBirth, email);
        }

        // This method presents account type options to the user and processes their selection to open a new account.
        public static void AccountTypeOption(AccountOpeningService accountOpeningService, Customer newCustomer)
        {
            UserInputOutput.AccountOpeningOptions();
            int accountType = UserInputOutput.GetUserintegerInput("Kindly select choice of Account: ");
            switch (accountType)
            {
                case 1:
                    try
                    {
                        var result = accountOpeningService.OpenNewSavingsAccount(newCustomer);
                        UserInputOutput.ShowMessage("Congratulations - savings account number: " + result.Message);
                    }
                    catch (Exception ex)
                    {
                        UserInputOutput.ShowMessage("Error: " + ex.Message);
                    }
                    break;

                case 2:
                    try
                    {
                        var result = accountOpeningService.OpenNewCurrentAccount(newCustomer);
                        UserInputOutput.ShowMessage("Congratulations - current account number: " + result.Message);
                    }
                    catch (Exception ex)
                    {
                        UserInputOutput.ShowMessage("Error: " + ex.Message);
                    }
                    break;

                default:
                    Console.Write("Invalid entry, try again: ");
                    break;
            }
        }

        // This method allows users to open additional accounts by selecting an existing account and choosing the type of
        // additional account they want to open.
        public static void AdditionalAccountOption(IAccountOpeningService accountOpeningService)
        {
            UserInputOutput.AccountOpeningOptions();
            int additionalAccountType = UserInputOutput.GetUserintegerInput("Kindly select choice of Account: ");
            switch (additionalAccountType)
            {
                case 1:
                    try
                    {
                        string existingAccountNumber = UserInputOutput.GetAccountString("Kindly enter existing account number: ");
                        //Account existingCurrentaccount = accountRepository.GetAccountByAccountNumber(existingAccountNumber);
                        var result = accountOpeningService.OpenAdditionalSavingsAccount(existingAccountNumber);

                        if (result.IsSuccess)
                        {
                            //Customer customer = customerRepository.GetCustomerById(existingCurrentaccount.CustomerID);
                            //customer.LinkAccountNumber(result.Message);

                            UserInputOutput.ShowMessage("Congrats - savings account: " + result.Message);
                        }
                        else
                            UserInputOutput.ShowMessage(result.Message);
                    }
                    catch (Exception ex)
                    {
                        UserInputOutput.ShowMessage("Error: " + ex.Message);
                    }
                    break;

                case 2:
                    try
                    {
                        string existingAccountNumber = UserInputOutput.GetAccountString("Kindly enter existing account number: ");
                        //Account existingSavingsaccount = accountRepository.GetAccountByAccountNumber(existingAccountNumber);
                        var result = accountOpeningService.OpenAdditionalCurrentAccount(existingAccountNumber); ;
                        if (result.IsSuccess)
                        {
                            //Customer customer = customerRepository.GetCustomerById(existingSavingsaccount.CustomerID);
                            //customer.LinkAccountNumber(result.Message);

                            UserInputOutput.ShowMessage("Congrats - current account: " + result.Message);
                        }
                        else
                            UserInputOutput.ShowMessage(result.Message);
                    }
                    catch (Exception ex)
                    {
                        UserInputOutput.ShowMessage("Error: " + ex.Message);
                    }
                    break;

                default:
                    Console.Write("Invalid entry, try again: ");
                    break;
            }
        }

        // This method handles the deposit transaction operation by collecting the account number and deposit amount from the user.
        public static void DepositTransactionOperation(IAccountOperationService accountOperationService)
        {
            try
            {
                string depositAccountNumber = UserInputOutput.GetAccountString("Kindly enter account number: ");
                decimal depositAmount = UserInputOutput.GetDecimalInput("Kindly enter amount to be deposited: ");

                var result = accountOperationService.Deposit(depositAccountNumber, depositAmount);

                if (result.IsFailure)
                    UserInputOutput.ShowMessage(result.Message);
                else
                    UserInputOutput.ShowMessage($"£{depositAmount}: Deposit transaction successsful");
            }
            catch (Exception ex)
            {
                UserInputOutput.ShowMessage("£rror: " + ex.Message);
            }
        }

        // This method handles the withdrawal transaction operation by collecting the account number and withdrawal amount from the user.
        public static void WithdrawalTransactionOperation(IAccountOperationService accountOperationService)
        {
            try
            {
                string withdrawalAccountNumber = UserInputOutput.GetAccountString("Kindly enter account number: ");
                decimal withdrawalAmount = UserInputOutput.GetDecimalInput("Kindly enter amount to be withdrawn: ");

                var result = accountOperationService.Withdraw(withdrawalAccountNumber, withdrawalAmount);

                if (result.IsFailure)
                    UserInputOutput.ShowMessage(result.Message);
                else
                    UserInputOutput.ShowMessage($"£{withdrawalAmount}: Withdrawal transaction successsful");
            }
            catch (Exception ex)
            {
                UserInputOutput.ShowMessage("Error: " + ex.Message);
            }
        }

        // This method retrieves and displays the account balance for a given account number entered by the user.
        public static void AccountBalanceOperation(IAccountQueryService accountQueryService)
        {
            try
            {
                string accountNumber = UserInputOutput.GetAccountString("Kindly enter account number: ");

                decimal balance = accountQueryService.GetAccountBalance(accountNumber);
                UserInputOutput.ShowMessage($"Your account balance is {balance}");
            }
            catch (Exception ex)
            {
                UserInputOutput.ShowMessage("Error: " + ex.Message);
            }
        }

        // This method retrieves and displays the account statement for a given account number entered by the user.
        public static void AccountStatementOperation(IAccountQueryService accountQueryService)
        {
            try
            {
                string accountNumber = UserInputOutput.GetAccountString("Kindly enter account number: ");
                IReadOnlyCollection<Transaction> transactions = accountQueryService.GetTransactions(accountNumber);

                if (transactions == null)
                {
                    UserInputOutput.ShowMessage("No transaction(s) on this account at the moment");
                    return;
                }

                Console.WriteLine("*******************************************************************");
                foreach (Transaction transaction in transactions)
                {
                    Console.WriteLine(transaction.ToString());
                }
            }
            catch (Exception ex)
            {
                UserInputOutput.ShowMessage("Error: Failed to load account statement");
            }
        }

        // This method allows users to update their customer profile information, such as last name and email address,
        // by selecting an existing account and providing the new information.
        public static void UpdateCustomerProfileOperation(ICustomerProfileService customerProfileService)
        {
            UserInputOutput.CustomerProfileUpdateOptions();
            int profileUpdateOption = UserInputOutput.GetUserintegerInput("Kindly select choice of Account: ");
            switch (profileUpdateOption)
            {
                case 1:
                    try
                    {
                        string existingAccountNumber = UserInputOutput.GetAccountString("Kindly enter existing account number: ");
                        string newLastname = UserInputOutput.GetUserInputString("Kindly enter your Lastname: ");
                        //Account customersAccount = accountRepository.GetAccountByAccountNumber(existingAccountNumber);
                        var result = customerProfileService.UpdateLastName(existingAccountNumber, newLastname);

                        if (result.IsFailure)
                            UserInputOutput.ShowMessage(result.Message);
                        else
                            UserInputOutput.ShowMessage("Lastname Updated Successfully");
                    }
                    catch (Exception ex)
                    {
                        UserInputOutput.ShowMessage("Error: Failed to update last name");
                    }

                    break;

                case 2:
                    try
                    {
                        string existingAccountNumber = UserInputOutput.GetAccountString("Kindly enter existing account number: ");
                        string newEmailAddress = UserInputOutput.GetUserEmailString("Kindly enter your new email address: ");
                        //Account customersAccount = accountRepository.GetAccountByAccountNumber(existingAccountNumber);

                        var result = customerProfileService.UpdateEmailAddress(existingAccountNumber, newEmailAddress);

                        if (result.IsFailure)
                            UserInputOutput.ShowMessage(result.Message);
                        else
                            UserInputOutput.ShowMessage("Email address Updated Successfully");
                    }
                    catch (Exception ex)
                    {
                        UserInputOutput.ShowMessage("Error: Failed to update email address");
                    }

                    break;

                default:
                    Console.Write("Invalid entry, try again: ");
                    break;
            }
        }

        // This method allows users to activate or deactivate their accounts by selecting an existing account
        // and choosing the desired operation.
        public static void AccountActivationAndDeactivationOperation(IAccountStatusService accountStatusService)
        {
            UserInputOutput.AccountStatusOperationOptions();
            int selection = UserInputOutput.GetUserintegerInput("Kindly select an option (1 - 2): ");
            switch (selection)
            {
                case 1:
                    try
                    {
                        string accountNumberForActivation = UserInputOutput.GetAccountString("Kindly enter your account number: ");
                        var result = accountStatusService.ActivateAccount(accountNumberForActivation);

                        if (result.IsFailure)
                            UserInputOutput.ShowMessage(result.Message);
                        else
                            UserInputOutput.ShowMessage("Account activated successfully");
                    }
                    catch (Exception ex)
                    {
                        UserInputOutput.ShowMessage("Error: Failed to activate account");
                    }

                    break;

                case 2:
                    try
                    {
                        string accountNumberForDeactivation = UserInputOutput.GetAccountString("Kindly enter your account number: ");
                        var result = accountStatusService.DeActivateAccount(accountNumberForDeactivation);

                        if (result.IsFailure)
                            UserInputOutput.ShowMessage(result.Message);
                        else
                            UserInputOutput.ShowMessage("Account deactivated successfully");
                    }
                    catch (Exception ex)
                    {
                        UserInputOutput.ShowMessage("Error: Failed to deactivate account");
                    }

                    break;

                default:
                    Console.Write("Invalid entry, try again: ");
                    break;
            }
        }
    }
}
