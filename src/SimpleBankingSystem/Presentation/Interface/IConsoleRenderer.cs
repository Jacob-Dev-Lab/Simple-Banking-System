using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Presentation.Enums;

namespace SimpleBankingSystem.Presentation.Interface
{
    public interface IConsoleRenderer
    {
        public void ShowHeader();

        public void ShowMainMenu();

        public void ShowAccountOpeningMenu();

        public void ShowProfileUpdateMenu();

        public void ShowAccountStatusMenu();

        public void ShowMessage(string message);

        public void ShowExitMessage();
    }
}
