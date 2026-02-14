using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.ErrorHandler;
using SimpleBankingSystem.Presentation.Enums;
using SimpleBankingSystem.Presentation.Interface;

namespace SimpleBankingSystem.Presentation.Oprations
{
    internal class NewAccountHandler (IUserInputReader userInputReader, 
        IConsoleRenderer consoleRenderer,
        ICreateCustomerService createCustomerService, 
        IAccountOpeningService accountOpeningService) : IAppStateHandler
    {
        private readonly IUserInputReader _userInputReader = userInputReader;
        private readonly IConsoleRenderer _consoleRenderer = consoleRenderer;
        private readonly ICreateCustomerService _createCustomerService = createCustomerService;
        private readonly IAccountOpeningService _accountOpeningService = accountOpeningService;

        public AppState State => AppState.OpenNewAccount;

        public AppState Handle()
        {
            try
            {
                var customerInformation = _userInputReader.ReadCustomer();

                var customer = _createCustomerService.CreateCustomer(
                    customerInformation.lastName,
                    customerInformation.otherNames,
                    customerInformation.dateOfBirth,
                    customerInformation.email);

                _consoleRenderer.ShowAccountOpeningMenu();

                var accountType = (AccountType)_userInputReader.ReadInt("Kindly select choice of Account: ");

                var result = accountType switch
                {
                    AccountType.Savings => _accountOpeningService.OpenNewSavingsAccount(customer),
                    AccountType.Current => _accountOpeningService.OpenNewCurrentAccount(customer),
                    _ => Result.Failure("Invalid entry, try again.")
                };

                _consoleRenderer.ShowMessage(result.Message ?? "Operation Failed.");

            }
            catch (Exception ex)
            {
                _consoleRenderer.ShowMessage("Error: " + ex.Message);
            }
                
            return AppState.MainMenu;
        }
    }
}
