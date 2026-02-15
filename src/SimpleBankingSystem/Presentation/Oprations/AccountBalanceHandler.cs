using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Presentation.Enums;
using SimpleBankingSystem.Presentation.Interface;

namespace SimpleBankingSystem.Presentation.Oprations
{
    internal class AccountBalanceHandler(IUserInputReader userInputReader,
        IConsoleRenderer consoleRenderer,
        IAccountQueryService accountQueryService) : IAppStateHandler
    {
        private readonly IUserInputReader _userInputReader = userInputReader;
        private readonly IConsoleRenderer _consoleRenderer = consoleRenderer;
        private readonly IAccountQueryService _accountQueryService = accountQueryService;

        public AppState State => AppState.CheckAccountBalance;

        public AppState Handle()
        {
            var accountNumber = _userInputReader.ReadAccountNumber("Enter Account Number: ");

            var result = _accountQueryService.GetAccountBalance(accountNumber);
            _consoleRenderer.ShowMessage(result.Message ?? "Operation Failed");

            return AppState.MainMenu;
        }
    }
}
