using SimpleBankingSystem.Presentation.ConsoleUI;
using SimpleBankingSystem.Presentation.Enums;
using SimpleBankingSystem.Presentation.Interface;

namespace SimpleBankingSystem.Presentation.Oprations
{
    internal class MainMenuHandler(IConsoleRenderer consoleRenderer) : IAppStateHandler
    {
        private readonly IConsoleRenderer _consoleRenderer = consoleRenderer;
        public AppState State => AppState.MainMenu;

        public AppState Handle()
        {
            _consoleRenderer.ShowMainMenu();

            var userInputReader = new UserInputReader();
            var userChoice = userInputReader.ReadInt("Please select an option from the menu: ");

            var appState = userChoice switch
            {
                1 => AppState.OpenNewAccount,
                2 => AppState.OpenAdditionalAccount,
                3 => AppState.MakeDeposit,
                4 => AppState.MakeWithdrawal,
                5 => AppState.CheckAccountBalance,
                6 => AppState.PrintAccountStatement,
                7 => AppState.UpdateCustomerProfile,
                8 => AppState.ChangeAccountStatus,
                9 => AppState.Exit,
                _ => AppState.MainMenu
            };

            return appState;
        }
    }
}
