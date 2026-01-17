using System.Net.Mail;

namespace SimpleBankingSystem.Presentation
{
    public static class UserInputOutput
    {
        public static string GetUserInputString(string prompt)
        {
            string value;
            bool valid;

            Console.Write(prompt);
            do
            {
                value = Console.ReadLine() ?? string.Empty;
                valid = !string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value) && !value.Any(char.IsDigit);

                if (!valid)
                    Console.WriteLine("Invalid Entry, try again: ");
            }
            while (!valid);

            return value;
        }

        public static DateOnly GetUserDateOfBirth(string prompt)
        {
            DateOnly dateOfBirth;

            Console.Write(prompt);

            while (!DateOnly.TryParse(Console.ReadLine(), out dateOfBirth))
            {
                Console.Write("Invalid entry, try again (YYYY-MM-DD): ");
            }

            return dateOfBirth;
        }

        public static string GetUserEmailString(string prompt)
        {
            string email;
            bool valid;

            Console.Write(prompt);
            do
            {
                email = Console.ReadLine() ?? string.Empty;
                valid = !string.IsNullOrWhiteSpace(email) && new MailAddress(email).Address == email;

                if (!valid)
                    Console.WriteLine("Invalid Entry, try again (example@gmail.com): ");
            }
            while (!valid);

            return email;
        }

        public static int GetMenuOptionSelection(string prompt, int min, int max)
        {
            int option;
            bool valid;

            Console.Write(prompt);
            do
            {
                valid = int.TryParse(Console.ReadLine(), out option) && (option >= min && option <= max);

                if (!valid)
                    Console.WriteLine($"Invalid Option, try again ({min} - {max}): ");
            }
            while (!valid);

            return option;
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("1. Open New Account");
            Console.WriteLine("2. Open an Additional Account");
            Console.WriteLine("3. Do a Deposit");
            Console.WriteLine("4. Do a Withdraw");
            Console.WriteLine("5. Check Account Balance");
            Console.WriteLine("6. Print Account Statement");
            Console.WriteLine("7. Update Customer Profile");
            Console.WriteLine("8. Activate/Deactivate Account");
            Console.WriteLine("9. Exit");
            Console.WriteLine();
        }

        public static void AccountOpeningOptions()
        {
            Console.WriteLine("Which account would you like to open");
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. Current Account");
        }

        public static void CustomerProfileUpdateOptions()
        {
            Console.WriteLine("1. Update Lastname (Surname)");
            Console.WriteLine("2. Update Email address");
        }

        public static void AccountStatusOperationOptions()
        {
            Console.WriteLine("1. Activate account");
            Console.WriteLine("2. Deactivate account");
        }

        public static void DisplayHeader()
        {
            Console.WriteLine("***************************************");
            Console.WriteLine("****** BANKING MANAGEMENT SYSTEM ******");
            Console.WriteLine("***************************************");
        }

        public static int GetUserintegerInput(string prompt)
        {
            int option;
            bool valid;

            Console.Write(prompt);
            do
            {
                valid = int.TryParse(Console.ReadLine(), out option);

                if (!valid)
                    Console.WriteLine($"Invalid Option, try again: ");
            }
            while (!valid);

            return option;
        }

        public static string GetAccountString(string prompt)
        {
            string accountNumber;
            bool valid;

            Console.Write(prompt);
            do
            {
                accountNumber = Console.ReadLine() ?? string.Empty;
                valid = !string.IsNullOrWhiteSpace(accountNumber);

                if (!valid)
                    Console.WriteLine("Invalid Entry, try again: ");
            }
            while (!valid);

            return accountNumber;
        }

        public static decimal GetDecimalInput(string prompt)
        {
            decimal value;
            bool valid;

            Console.Write(prompt);
            do
            {
                valid = decimal.TryParse(Console.ReadLine(), out value) && value > 0;

                if (!valid)
                    Console.WriteLine("Invalid Entry, try again: ");
            }
            while (!valid);

            return value;
        }

    }
}
