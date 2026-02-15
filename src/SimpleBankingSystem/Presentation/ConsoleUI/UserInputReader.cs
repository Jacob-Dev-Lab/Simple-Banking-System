using System.Net.Mail;
using SimpleBankingSystem.Presentation.Interface;

namespace SimpleBankingSystem.Presentation.ConsoleUI
{
    internal class UserInputReader : IUserInputReader
    {
        public string ReadString(string prompt)
        {
            string value;
            bool valid;
            Console.Write(prompt);

            do
            {
                value = Console.ReadLine() ?? string.Empty;
                valid = !string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value) && !value.Any(char.IsDigit);

                if (!valid)
                    Console.Write("Invalid Entry, try again: ");
            }
            while (!valid);

            return value;
        }

        public DateOnly ReadDateOfBirth(string prompt)
        {
            DateOnly dateOfBirth;
            Console.Write(prompt);

            while (!DateOnly.TryParse(Console.ReadLine(), out dateOfBirth))
            {
                Console.Write("Invalid entry, try again (YYYY-MM-DD): ");
                continue;
            }

            return dateOfBirth;
        }

        public string? ReadEmailString(string prompt)
        {
            MailAddress? email;
            bool valid;
            Console.Write(prompt);

            do
            {
                valid = MailAddress.TryCreate(Console.ReadLine(), out email);

                if (!valid)
                    Console.Write("Invalid Entry, try again (example@gmail.com): ");
            }
            while (!valid);

            return email?.Address;
        }

        public int ReadInt(string prompt)
        {
            int input;
            bool valid;

            Console.Write(prompt);
            do
            {
                valid = int.TryParse(Console.ReadLine(), out input);

                if (!valid)
                    Console.Write($"Invalid Option, try again: ");
            }
            while (!valid);

            return input;
        }

        public string ReadAccountNumber(string prompt)
        {
            string accountNumber;
            bool valid;

            Console.Write(prompt);
            do
            {
                accountNumber = Console.ReadLine() ?? string.Empty;
                valid = !string.IsNullOrWhiteSpace(accountNumber);

                if (!valid)
                    Console.Write("Invalid Entry, try again: ");
            }
            while (!valid);

            return accountNumber;
        }

        public decimal ReadDecimal(string prompt)
        {
            decimal value;
            bool valid;

            Console.Write(prompt);
            do
            {
                valid = decimal.TryParse(Console.ReadLine(), out value);

                if (!valid)
                    Console.Write("Invalid Entry, try again: ");
            }
            while (!valid);

            return value;
        }

        public (string lastName, string otherNames, DateOnly dateOfBirth, string? email) ReadCustomer()
        {
            string lastName = ReadString("Kindly enter Lastname: ");
            string otherNames = ReadString("Kindly enter other names: ");
            DateOnly dateOfBirth = ReadDateOfBirth("Kindly enter date of birth (YYYY-MM-DD): ");
            string? email = ReadEmailString("Kindly enter email address (example@gmail.com): ");

            return (lastName, otherNames, dateOfBirth, email);
        }
    }
}
