using SimpleBankingSystem.Interfaces;
using SimpleBankingSystem.Presentation;
using SimpleBankingSystem.Repositories;
using SimpleBankingSystem.Service.Account;
using SimpleBankingSystem.Service.Customer;

namespace SimpleBankingSystem
{
    class Program
    {
        public static void Main(string[] args)
        {
            ICustomerRepository customerRepository = new CustomerRepository();
            IAccountRepository accountRepository = new AccountRepository();
            IGenerateAccountNumber generateAccount = new GuidAccountNumber();
            ITransactionRepository transactionRepository = new TransactionRepository();

            AccountOpeningService accountOpeningService = new AccountOpeningService(generateAccount, accountRepository);
            AccountOperationService accountOperationService = new AccountOperationService(accountRepository, transactionRepository);
            AccountQueryService accountQueryService = new AccountQueryService(accountRepository, transactionRepository);
            AccountStatusService accountStatusService = new AccountStatusService(accountRepository);
            CustomerProfileService customerProfileService = new CustomerProfileService(customerRepository);

            bool startApplication = true;
            do
            {
                UserInputOutput.DisplayHeader();
                UserInputOutput.DisplayMenu();
                int option = UserInputOutput.GetMenuOptionSelection("Kindly enter an option: ", 1, 9);
                Console.WriteLine();

                switch (option)
                {
                    case 1:
                        string lastName = UserInputOutput.GetUserInputString("Kindly enter Lastname: ");
                        string otherNames = UserInputOutput.GetUserInputString("Kindly enter other names: ");
                        DateOnly dateOfBirth = UserInputOutput.GetUserDateOfBirth("Kindly enter date of birth (YYYY-MM-DD): ");
                        string email = UserInputOutput.GetUserInputString("Kindly enter email address (example@gmail.com): ");

                        Customer newCustomer = new(lastName, otherNames, dateOfBirth, email);

                        UserInputOutput.AccountOpeningOptions();
                        int accountType = UserInputOutput.GetUserintegerInput("Kindly select choice of Account: ");
                        switch (accountType)
                        {
                            case 1:
                                try
                                {
                                    customerRepository.Add(newCustomer);
                                    string accountNumber = accountOpeningService.OpenSavingsAccount(newCustomer.CustomerId);
                                    newCustomer.LinkAccountNumber(accountNumber);

                                    Console.WriteLine($"Congratulations... your savings account is {accountNumber}");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;

                            case 2:
                                try
                                {
                                    customerRepository.Add(newCustomer);
                                    string accountNumber = accountOpeningService.OpenCurrentAccount(newCustomer.CustomerId);
                                    newCustomer.LinkAccountNumber(accountNumber);

                                    Console.WriteLine($"Congratulations... your current account is {accountNumber}");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;

                            default:
                                Console.WriteLine("Invalid entry, try again: ");
                                break;
                        }
                        Console.WriteLine();

                        break;
                    case 2:
                        UserInputOutput.AccountOpeningOptions();
                        int additionalAccountType = UserInputOutput.GetUserintegerInput("Kindly select choice of Account: ");
                        switch (additionalAccountType)
                        {
                            case 1:
                                try
                                {
                                    string existingAccountNumber = UserInputOutput.GetAccountString("Kindly enter existing account number: ");
                                    Account? existingCurrentaccount = accountRepository.GetByNumber(existingAccountNumber);

                                    if (existingCurrentaccount == null)
                                    {
                                        Console.WriteLine("Account number not valid.");
                                        break;
                                    }

                                    string newSavingsAccountNumber = accountOpeningService.OpenSavingsAccount(existingCurrentaccount.CustomerID);
                                    Console.WriteLine($"Congratulations... your savings account is {newSavingsAccountNumber}");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;

                            case 2:
                                try
                                {
                                    string existingAccountNumber = UserInputOutput.GetAccountString("Kindly enter existing account number: ");
                                    Account? existingSavingsaccount = accountRepository.GetByNumber(existingAccountNumber);

                                    if (existingSavingsaccount == null)
                                    {
                                        Console.WriteLine("Account number not valid.");
                                        break;
                                    }

                                    string newCurrentAccountNumber = accountOpeningService.OpenCurrentAccount(existingSavingsaccount.CustomerID);
                                    Console.WriteLine($"Congratulations... your current account is {newCurrentAccountNumber}");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;

                            default:
                                Console.WriteLine("Invalid entry, try again: ");
                                break;
                        }
                        Console.WriteLine();

                        break;
                    case 3:
                        try
                        {
                            string depositAccountNumber = UserInputOutput.GetAccountString("Kindly enter account number: ");
                            decimal depositAmount = UserInputOutput.GetDecimalInput("Kindly enter deposit amount: ");
                            accountOperationService.Deposit(depositAccountNumber, depositAmount);
                            Console.WriteLine($"{depositAmount} Deposit transaction successsful");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.WriteLine();

                        break;
                    case 4:
                        try
                        {
                            string withdrawalAccountNumber = UserInputOutput.GetAccountString("Kindly enter account number: ");
                            decimal withdrawalAmount = UserInputOutput.GetDecimalInput("Kindly enter deposit amount: ");
                            accountOperationService.Deposit(withdrawalAccountNumber, withdrawalAmount);
                            Console.WriteLine($"{withdrawalAmount} Withdrawal transaction successsful");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.WriteLine();

                        break;
                    case 5:
                        try
                        {
                            string accountNumber = UserInputOutput.GetAccountString("Kindly enter account number: ");
                            decimal balance = accountQueryService.GetAccountBalance(accountNumber);
                            Console.WriteLine($"Your account balance is {balance}");
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.WriteLine();

                        break;
                    case 6:
                        try
                        {
                            string accountNumber = UserInputOutput.GetAccountString("Kindly enter account number: ");
                            IReadOnlyCollection<Transaction> transactions = accountQueryService.GetTransactions(accountNumber);

                            if (transactions.Count == 0)
                            {
                                Console.WriteLine("No transaction(s) on this account at the moment");
                                break;
                            }

                            Console.WriteLine("*******************************************************************");
                            foreach (Transaction transaction in transactions)
                            {
                                Console.WriteLine($"{transaction.TransactionDate} - {transaction.TransactionID} | {transaction.TransactionType} | {transaction.Amount}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.WriteLine();

                        break;
                    case 7:
                        UserInputOutput.CustomerProfileUpdateOptions();
                        int profileUpdateOption = UserInputOutput.GetUserintegerInput("Kindly select choice of Account: ");
                        switch (profileUpdateOption)
                        {
                            case 1:
                                try
                                {
                                    string newLastname = UserInputOutput.GetUserInputString("Kindly enter your Lastname: ");
                                    string existingAccountNumber = UserInputOutput.GetAccountString("Kindly enter existing account number: ");
                                    Account? customersAccount = accountRepository.GetByNumber(existingAccountNumber);

                                    if (customersAccount == null)
                                    {
                                        Console.WriteLine("Account number not valid.");
                                        break;
                                    }

                                    customerProfileService.UpdateLastName(customersAccount.CustomerID, newLastname);
                                    Console.WriteLine($"Lastname Updated Successfully");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                                break;

                            case 2:
                                try
                                {
                                    string newEmailAddress = UserInputOutput.GetUserEmailString("Kindly enter your new email address: ");
                                    string existingAccountNumber = UserInputOutput.GetAccountString("Kindly enter existing account number: ");
                                    Account? customersAccount = accountRepository.GetByNumber(existingAccountNumber);

                                    if (customersAccount == null)
                                    {
                                        Console.WriteLine("Account number not valid.");
                                        break;
                                    }

                                    customerProfileService.UpdateEmailAddress(customersAccount.CustomerID, newEmailAddress);
                                    Console.WriteLine($"Email address Updated Successfully");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                                break;

                            default:
                                Console.WriteLine("Invalid entry, try again: ");
                                break;
                        }
                        Console.WriteLine();

                        break;
                    case 8:
                        UserInputOutput.AccountStatusOperationOptions();
                        int selection = UserInputOutput.GetUserintegerInput("Kindly select an option (1 - 2): ");
                        switch (selection)
                        {
                            case 1:
                                try
                                {
                                    string accountNumberForActivation = UserInputOutput.GetAccountString("Kindly enter your account number: ");
                                    accountStatusService.ActivateAccount(accountNumberForActivation);
                                    Console.WriteLine("Account activated successfully");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                                break;

                            case 2:
                                try
                                {
                                    string accountNumberForDeactivation = UserInputOutput.GetAccountString("Kindly enter your account number: ");
                                    accountStatusService.DeActivateAccount(accountNumberForDeactivation);
                                    Console.WriteLine("Account deactivated successfully");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                                break;

                            default:
                                Console.WriteLine("Invalid entry, try again");
                                break;
                        }
                        Console.WriteLine();

                        break;
                    case 9:
                        startApplication = false;
                        break;
                    default:
                        Console.WriteLine("Invalid entry, kindly enter a valid option");
                        break;
                }
            }
            while (startApplication);
        }
    }
}