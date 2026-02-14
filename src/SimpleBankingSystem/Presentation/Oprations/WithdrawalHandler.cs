using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Presentation.Enums;
using SimpleBankingSystem.Presentation.Interface;

namespace SimpleBankingSystem.Presentation.Oprations
{
    internal class WithdrawalHandler(IUserInputReader userInputReader,
        IConsoleRenderer consoleRenderer,
        IAccountOperationService accountOperationService) : IAppStateHandler
    {
        private readonly IUserInputReader _userInputReader = userInputReader;
        private readonly IConsoleRenderer _consoleRenderer = consoleRenderer;
        private readonly IAccountOperationService _accountOperationService = accountOperationService;

        public AppState State => AppState.MakeWithdrawal;

        public AppState Handle()
        {
            
            try
            {
                var accountNumber = _userInputReader.ReadAccountNumber("Enter Account Number: ");
                var amount = _userInputReader.ReadDecimal("Enter amount to be withdrawn: ");

                var result = _accountOperationService.Withdraw(accountNumber, amount);

                _consoleRenderer.ShowMessage(result.Message ?? "Transaction Failed.");
            }
            catch (Exception ex)
            {
                _consoleRenderer.ShowMessage("Error: " + ex.Message);
            }

            return AppState.MainMenu;
        }
    }
}
