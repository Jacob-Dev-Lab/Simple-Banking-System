using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Presentation.Enums;
using SimpleBankingSystem.Presentation.Interface;
using SimpleBankingSystem.Utilities;

namespace SimpleBankingSystem.Presentation.Oprations
{
    internal class AccountStatementHandler(IUserInputReader userInputReader,
        IConsoleRenderer consoleRenderer,
        IAccountQueryService accountQueryService) : IAppStateHandler
    {
        private readonly IUserInputReader _userInputReader = userInputReader;
        private readonly IConsoleRenderer _consoleRenderer = consoleRenderer;
        private readonly IAccountQueryService _accountQueryService = accountQueryService;

        public AppState State => AppState.PrintAccountStatement;

        public AppState Handle()
        {
            try
            {
                var accountNumber = _userInputReader.ReadAccountNumber("Enter Account Number: ");
                var transactions = _accountQueryService.GetTransactions(accountNumber);

                if (transactions == null)
                {
                    _consoleRenderer.ShowMessage("No transaction(s) on this account at the moment");
                    Thread.Sleep(3000);
                    return AppState.MainMenu;
                }

                Console.WriteLine("*******************************************************************");
                foreach (var transaction in transactions)
                {
                    _consoleRenderer.ShowMessage(transaction.ToString());
                }
            }
            catch (Exception)
            {
                _consoleRenderer.ShowMessage("Error: Failed to load account statement");
            }

            return AppState.MainMenu;
        }
    }
}
