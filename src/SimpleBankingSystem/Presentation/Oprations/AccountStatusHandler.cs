using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.ErrorHandler;
using SimpleBankingSystem.Presentation.Enums;
using SimpleBankingSystem.Presentation.Interface;

namespace SimpleBankingSystem.Presentation.Oprations
{
    internal class AccountStatusHandler(IUserInputReader userInputReader,
        IConsoleRenderer consoleRenderer,
        IAccountStatusService accountStatusService) : IAppStateHandler
    {
        private readonly IUserInputReader _userInputReader = userInputReader;
        private readonly IConsoleRenderer _consoleRenderer = consoleRenderer;
        private readonly IAccountStatusService _accountStatusService = accountStatusService;

        public AppState State => AppState.ChangeAccountStatus;

        public AppState Handle()
        {
            try
            {
                var accountNumber = _userInputReader.ReadAccountNumber("Enter account number: ");
                _consoleRenderer.ShowAccountStatusMenu();

                var options = (AccountState)_userInputReader.ReadInt("Select an option: ");
                var result = options switch
                {
                    AccountState.Activate => _accountStatusService.ActivateAccount(accountNumber),
                    AccountState.Deactivate => _accountStatusService.DeActivateAccount(accountNumber),
                    _ => Result.Failure("Invalid Entry...")
                };
                    
                _consoleRenderer.ShowMessage(result.Message ?? "Operation Failed");
            }
            catch (Exception ex)
            {
                _consoleRenderer.ShowMessage("Error: " + ex.Message);
            }

            return AppState.MainMenu;
        }
    }
}
