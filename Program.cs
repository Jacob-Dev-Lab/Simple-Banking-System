using SimpleBankingSystem.Presentation;
using SimpleBankingSystem.Utilities;
using SimpleBankingSystem.Application.Service.Account;
using SimpleBankingSystem.Application.Service.Customer;
using SimpleBankingSystem.Infrastructure.Interfaces;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Infrastructure.Repositories;
using SimpleBankingSystem.Domain;

namespace SimpleBankingSystem
{
    class Program
    {
        public static void Main(string[] args)
        {
            var connection = new FileConnection();
            var (customerPath, accountPath, transactionPath) = connection.ConnectionString();

            string customerFile = customerPath;
            string accountFile = accountPath;
            string transactionFile = transactionPath;

            ICustomerRepository customerRepository = new CustomerFileRepository(customerFile);
            IAccountRepository accountRepository = new AccountFileRepository(accountFile);
            ITransactionRepository transactionRepository = new TransactionFileRepository(transactionFile);
            IGenerateAccountNumber generateAccount = new GuidAccountNumber();

            customerRepository.Load();
            accountRepository.Load();
            transactionRepository.Load();

            AccountOpeningService accountOpeningService = new (accountRepository, generateAccount);
            AccountOperationService accountOperationService = new (accountRepository, transactionRepository);
            AccountQueryService accountQueryService = new (accountRepository, transactionRepository);
            AccountStatusService accountStatusService = new (accountRepository);
            CustomerProfileService customerProfileService = new (customerRepository);

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
                                    customerRepository.Save();
                                    Console.WriteLine($"Congratulations... your savings account number is {accountNumber}");
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
                                    customerRepository.Save();
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
                                    Account existingCurrentaccount = accountRepository.GetAccountByAccountNumber(existingAccountNumber);
                                    string newSavingsAccountNumber = accountOpeningService.OpenSavingsAccount(existingCurrentaccount.CustomerID);
                                    Customer customer = customerRepository.GetCustomerById(existingCurrentaccount.CustomerID);
                                    customer.LinkAccountNumber(newSavingsAccountNumber);
                                    Console.WriteLine($"Congratulations... your savings account number is {newSavingsAccountNumber}");
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
                                    Account existingSavingsaccount = accountRepository.GetAccountByAccountNumber(existingAccountNumber);
                                    string newCurrentAccountNumber = accountOpeningService.OpenCurrentAccount(existingSavingsaccount.CustomerID);
                                    Customer customer = customerRepository.GetCustomerById(existingSavingsaccount.CustomerID);
                                    customer.LinkAccountNumber(newCurrentAccountNumber);
                                    Console.WriteLine($"Congratulations... your current account number is {newCurrentAccountNumber}");
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
                            decimal depositAmount = UserInputOutput.GetDecimalInput("Kindly enter amount to be deposited: ");
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
                            decimal withdrawalAmount = UserInputOutput.GetDecimalInput("Kindly enter amount to be withdrawn: ");
                            accountOperationService.Withdraw(withdrawalAccountNumber, withdrawalAmount);
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
                                Console.WriteLine(transaction.ToString());
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
                                    string existingAccountNumber = UserInputOutput.GetAccountString("Kindly enter existing account number: ");
                                    string newLastname = UserInputOutput.GetUserInputString("Kindly enter your Lastname: ");
                                    Account customersAccount = accountRepository.GetAccountByAccountNumber(existingAccountNumber);
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
                                    string existingAccountNumber = UserInputOutput.GetAccountString("Kindly enter existing account number: ");
                                    string newEmailAddress = UserInputOutput.GetUserEmailString("Kindly enter your new email address: ");
                                    Account customersAccount = accountRepository.GetAccountByAccountNumber(existingAccountNumber);

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