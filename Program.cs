using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Application.Service.AccountService;
using SimpleBankingSystem.Application.Service.CustomerService;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Infrastructure.Repositories;
using SimpleBankingSystem.Presentation;
using SimpleBankingSystem.Utilities;

namespace SimpleBankingSystem
{
    class Program
    {
        public static void Main(string[] args)
        {
            var connection = new FileConnection();
            var (customerPath, accountPath, transactionPath, logPath) = connection.ConnectionString();

            string customerFile = customerPath;
            string accountFile = accountPath;
            string transactionFile = transactionPath;
            string LogFile = logPath;

            ILogger logger = new Logger(logPath);
            ICustomerRepository customerRepository = new FileCustomerRepository(customerFile, logger);
            IAccountRepository accountRepository = new FileAccountRepository(accountFile, logger);
            ITransactionRepository transactionRepository = new FileTransactionRepository(transactionFile, logger);
            IGenerateAccountNumber generateAccount = new GuidAccountNumber();

            customerRepository.Load();
            accountRepository.Load();
            transactionRepository.Load();

            AccountOpeningService accountOpeningService = new (accountRepository, customerRepository, generateAccount);
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
                        var customerData = IOHelper.CustomerInformation();

                        Customer newCustomer = new(
                            customerData.lastName, customerData.otherNames, 
                            customerData.dateOfBirth, customerData.email
                        );

                        IOHelper.AccountTypeOption(accountOpeningService, newCustomer);
                        Console.WriteLine();
                        break;

                    case 2:
                        IOHelper.AdditionalAccountOption(accountOpeningService, accountRepository, customerRepository);
                        Console.WriteLine();
                        break;

                    case 3:
                        IOHelper.DepositTransactionOperation(accountOperationService);
                        Console.WriteLine();
                        break;

                    case 4:
                        IOHelper.WithdrawalTransactionOperation(accountOperationService);
                        Console.WriteLine();
                        break;

                    case 5:
                        IOHelper.AccountBalanceOperation(accountQueryService);
                        Console.WriteLine();
                        break;

                    case 6:
                        IOHelper.AccountStatementOperation(accountQueryService);
                        Console.WriteLine();
                        break;

                    case 7:
                        IOHelper.UpdateCustomerProfileOperation(customerProfileService, accountRepository);
                        Console.WriteLine();
                        break;

                    case 8:
                        IOHelper.AccountActivationAndDeactivationOperation(accountStatusService);
                        Console.WriteLine();
                        break;

                    case 9:
                        startApplication = false;
                        break;
                    default:
                        Console.Write("Invalid entry, kindly enter a valid option: ");
                        break;
                }
            }
            while (startApplication);
        }
    }
}