using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Presentation.Enums;
using SimpleBankingSystem.Presentation.Interface;

namespace SimpleBankingSystem.Presentation.Oprations
{
    internal class DepositHandler(IUserInputReader userInputReader,
        IConsoleRenderer consoleRenderer,
        IAccountOperationService accountOperationService) : IAppStateHandler
    {
        private readonly IUserInputReader _userInputReader = userInputReader;
        private readonly IConsoleRenderer _consoleRenderer = consoleRenderer;
        private readonly IAccountOperationService _accountOperationService = accountOperationService;

        public AppState State => AppState.MakeDeposit;

        public AppState Handle()
        {
            var accountNumber = _userInputReader.ReadAccountNumber("Enter Account Number: ");
            var amount = _userInputReader.ReadDecimal("Enter amount to be deposited: "); 

            var result = _accountOperationService.Deposit(accountNumber, amount);

            _consoleRenderer.ShowMessage(result.Message ?? "Operation Failed");

            return AppState.MainMenu;
        }
    }
}
