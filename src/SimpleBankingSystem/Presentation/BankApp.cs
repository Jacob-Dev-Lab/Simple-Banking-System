using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Application.Service.AccountService;
using SimpleBankingSystem.Application.Service.CustomerService;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Utilities;

namespace SimpleBankingSystem.Presentation
{
    /* The BankApp class serves as the main entry point for the banking application,
     * providing a user interface for various banking operations such as account opening,
     * transactions, and profile management. It implements the IBankApp interface and
     * utilizes several services to perform the required operations based on user input.*/
    public class BankApp(IAccountOpeningService accountOpeningService,
        IAccountOperationService accountOperationService,
        IAccountQueryService accountQueryService,
        IAccountStatusService accountStatusService,
        ICustomerProfileService customerProfileService) : IBankApp
    {

        private readonly IAccountOpeningService _accountOpeningService = accountOpeningService;
        private readonly IAccountOperationService _accountOperationService = accountOperationService;
        private readonly IAccountQueryService _accountQueryService = accountQueryService;
        private readonly IAccountStatusService _accountStatusService = accountStatusService;
        private readonly ICustomerProfileService _customerProfileService = customerProfileService;
        public void Run()
        {
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
                        IOHelper.AccountTypeOption(_accountOpeningService, newCustomer);
                        Console.WriteLine();
                        break;

                    case 2:
                        // Allow the user to open an additional account for an existing customer
                        IOHelper.AdditionalAccountOption(_accountOpeningService);
                        Console.WriteLine();
                        break;

                    case 3:
                        // Perform a deposit transaction operation based on user input
                        IOHelper.DepositTransactionOperation(_accountOperationService);
                        Console.WriteLine();
                        break;

                    case 4:
                        // Perform a withdrawal transaction operation based on user input
                        IOHelper.WithdrawalTransactionOperation(_accountOperationService);
                        Console.WriteLine();
                        break;

                    case 5:
                        // Display the account balance for a specified account based on user input
                        IOHelper.AccountBalanceOperation(_accountQueryService);
                        Console.WriteLine();
                        break;

                    case 6:
                        // Display the account statement for a specified account based on user input
                        IOHelper.AccountStatementOperation(_accountQueryService);
                        Console.WriteLine();
                        break;

                    case 7:
                        // Allow the user to update their customer profile information
                        IOHelper.UpdateCustomerProfileOperation(_customerProfileService);
                        Console.WriteLine();
                        break;

                    case 8:
                        // Allow the user to activate or deactivate an account based on their selection
                        IOHelper.AccountActivationAndDeactivationOperation(_accountStatusService);
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
