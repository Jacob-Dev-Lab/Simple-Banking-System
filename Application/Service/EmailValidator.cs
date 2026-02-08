using System.Net.Mail;

namespace SimpleBankingSystem.Application.Service
{
    public static class EmailValidator
    {
        /* This method validates an email address. It first checks if the email string 
         * is null, empty, or consists only of whitespace characters. If any of these 
         * conditions are true, it throws an ArgumentException indicating that a valid 
         * email is required. Then, it attempts to create a new MailAddress object using 
         * the provided email string. If the format of the email is invalid, 
         * a FormatException will be thrown, which is caught and rethrown as an ArgumentException 
         * with a message indicating that the email format is invalid. */

        public static void Validate(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Valid email required", nameof(email));

            try
            {
                _ = new MailAddress(email).Address;
            }
            catch(FormatException)
            {
                throw new ArgumentException("Invalid email format", nameof(email));
            }
        }
    }
}
