using System.Security.Authentication.ExtendedProtection;
using Microsoft.Extensions.DependencyInjection;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Application.Service;
using SimpleBankingSystem.Application.Service.AccountService;
using SimpleBankingSystem.Application.Service.CustomerService;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Infrastructure.Repositories;
using SimpleBankingSystem.Infrastructure.Service;
using SimpleBankingSystem.Presentation;
using SimpleBankingSystem.Utilities;

namespace SimpleBankingSystem
{
    class Program
    {
        public static void Main(string[] args)
        {
            var connection = new FileConnection();

            // Initialize logger and repositories with file paths from the connection
            ILogger logger = new Logger(connection);
            ICustomerRepository customerRepository = new FileCustomerRepository(connection, logger);
            IAccountRepository accountRepository = new FileAccountRepository(connection, logger);
            ITransactionRepository transactionRepository = new FileTransactionRepository(connection, logger);
            IGenerateAccountNumber generateAccount = new GuidAccountNumber();

            // Load existing data from files into repositories
            customerRepository.Load();
            accountRepository.Load();
            transactionRepository.Load();

            // Initialize services with the repositories and other dependencies
            AccountOpeningService accountOpeningService = new(accountRepository, customerRepository, generateAccount);
            AccountOperationService accountOperationService = new(accountRepository, transactionRepository);
            AccountQueryService accountQueryService = new(accountRepository, transactionRepository);
            AccountStatusService accountStatusService = new(accountRepository);
            CustomerProfileService customerProfileService = new (customerRepository, accountRepository);


            // Start the application loop to interact with the user
            bool startApplication = true;
            do
            {
                // Display the application header and menu options to the user
                UserInputOutput.DisplayHeader();
                UserInputOutput.DisplayMenu();

                // Get the user's menu option selection
                int option = UserInputOutput.GetMenuOptionSelection("Kindly enter an option: ", 1, 9);
                Console.WriteLine();

                switch (option) // Handle the user's menu selection and perform the corresponding operation
                {
                    case 1:
                        // Collect customer information from the user and create a new customer
                        var customerData = IOHelper.CustomerInformation();

                        Customer newCustomer = new(
                            customerData.lastName, 
                            customerData.otherNames, 
                            customerData.dateOfBirth, 
                            customerData.email
                        );

                        // Open a new account for the customer based on the collected information
                        IOHelper.AccountTypeOption(accountOpeningService, newCustomer);
                        Console.WriteLine();
                        break;

                    case 2:
                        // Allow the user to open an additional account for an existing customer
                        IOHelper.AdditionalAccountOption(accountOpeningService);
                        Console.WriteLine();
                        break;

                    case 3:
                        // Perform a deposit transaction operation based on user input
                        IOHelper.DepositTransactionOperation(accountOperationService);
                        Console.WriteLine();
                        break;

                    case 4:
                        // Perform a withdrawal transaction operation based on user input
                        IOHelper.WithdrawalTransactionOperation(accountOperationService);
                        Console.WriteLine();
                        break;

                    case 5:
                        // Display the account balance for a specified account based on user input
                        IOHelper.AccountBalanceOperation(accountQueryService);
                        Console.WriteLine();
                        break;

                    case 6:
                        // Display the account statement for a specified account based on user input
                        IOHelper.AccountStatementOperation(accountQueryService);
                        Console.WriteLine();
                        break;

                    case 7:
                        // Allow the user to update their customer profile information
                        IOHelper.UpdateCustomerProfileOperation(customerProfileService);
                        Console.WriteLine();
                        break;

                    case 8:
                        // Allow the user to activate or deactivate an account based on their selection
                        IOHelper.AccountActivationAndDeactivationOperation(accountStatusService);
                        Console.WriteLine();
                        break;

                    case 9:
                        // Exit the application loop and terminate the program
                        startApplication = false;
                        break;

                    default:
                        // Handle invalid menu options by prompting the user to enter a valid option
                        Console.Write("Invalid entry, kindly enter a valid option: ");
                        break;
                }
            }
            while (startApplication);
        }
    }
}