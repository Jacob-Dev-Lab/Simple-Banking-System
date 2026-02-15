using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.Enums;
using SimpleBankingSystem.Domain.ErrorHandler;
using SimpleBankingSystem.Presentation.Enums;
using SimpleBankingSystem.Presentation.Interface;

namespace SimpleBankingSystem.Presentation.Oprations
{
    internal class AdditionalAccountHandler(IUserInputReader userInputReader,
        IConsoleRenderer consoleRenderer,
        IAccountOpeningService accountOpeningService) : IAppStateHandler
    {
        private readonly IUserInputReader _userInputReader = userInputReader;
        private readonly IConsoleRenderer _consoleRenderer = consoleRenderer;
        private readonly IAccountOpeningService _accountOpeningService = accountOpeningService;

        public AppState State => AppState.OpenAdditionalAccount;

        public AppState Handle()
        {
            var accountNumber = _userInputReader.ReadAccountNumber("Enter Account Number: ");
            _consoleRenderer.ShowAccountOpeningMenu();

            var accountType = (AccountType)_userInputReader.ReadInt("Kindly select choice of Account: ");

            var result = accountType switch
            {
                AccountType.Savings => _accountOpeningService.OpenAdditionalSavingsAccount(accountNumber),
                AccountType.Current => _accountOpeningService.OpenAdditionalCurrentAccount(accountNumber),
                _ => Result.Failure("Invalid entry, try again.")
            };

            _consoleRenderer.ShowMessage(result.Message ?? "Operation Failed.");

            return AppState.MainMenu;
        }
    }
}
