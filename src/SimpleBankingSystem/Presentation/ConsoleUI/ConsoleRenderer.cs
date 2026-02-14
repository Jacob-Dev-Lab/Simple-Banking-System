using SimpleBankingSystem.Presentation.Enums;
using SimpleBankingSystem.Presentation.Interface;

namespace SimpleBankingSystem.Presentation.ConsoleUI
{
    internal class ConsoleRenderer : IConsoleRenderer
    {
        public void ShowHeader()
        {
            Console.WriteLine("***************************************");
            Console.WriteLine("****** BANKING MANAGEMENT SYSTEM ******");
            Console.WriteLine("***************************************");
        }

        public void ShowMainMenu()
        {
            Console.WriteLine("==================Main Menu Options==========================");
            Console.WriteLine("1. Open New Account");
            Console.WriteLine("2. Open an Additional Account");
            Console.WriteLine("3. Make a Deposit");
            Console.WriteLine("4. Make a Withdraw");
            Console.WriteLine("5. Check Account Balance");
            Console.WriteLine("6. Print Account Statement");
            Console.WriteLine("7. Update Customer Profile");
            Console.WriteLine("8. Activate/Deactivate Account");
            Console.WriteLine("9. Exit");
            Console.WriteLine();
        }

        public void ShowAccountOpeningMenu()
        {
            Console.WriteLine("Which account would you like to open");
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. Current Account");
        }

        public void ShowProfileUpdateMenu()
        {
            Console.WriteLine("1. Update Lastname (Surname)");
            Console.WriteLine("2. Update Email address");
        }

        public void ShowAccountStatusMenu()
        {
            Console.WriteLine("1. Activate account");
            Console.WriteLine("2. Deactivate account");
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
        }

        public void ShowExitMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Thank you for Bannking with us.");
        }
    }
}
