using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Presentation.ConsoleUI;
using SimpleBankingSystem.Presentation.Enums;
using SimpleBankingSystem.Presentation.Interface;
using SimpleBankingSystem.Presentation.Oprations;

namespace SimpleBankingSystem.Presentation
{
    /* The BankApp class serves as the main entry point for the banking application,
     * providing a user interface for various banking operations such as account opening,
     * transactions, and profile management. It implements the IBankApp interface and
     * utilizes several services to perform the required operations based on user input.*/
    public class BankApp(IEnumerable<IAppStateHandler> appStateHandlers) : IBankApp
    {
        private readonly Dictionary<AppState, IAppStateHandler> _handler = appStateHandlers.ToDictionary(h => h.State);

        public void Run()
        {
            var consoleRenderer = new ConsoleRenderer();
            consoleRenderer.ShowHeader();

            var currentState = AppState.MainMenu;

            while (currentState != AppState.Exit)
            {
                var handler = _handler[currentState];

                if (handler != null)
                    currentState = handler.Handle();
            }

        }
    }
}
