using System.Net.Mail;

namespace SimpleBankingSystem.Application.Service
{
    public static class EmailValidator
    {
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
