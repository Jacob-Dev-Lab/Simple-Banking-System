using SimpleBankingSystem.Domain.Interfaces;

namespace SimpleBankingSystem.Infrastructure.Service
{
    public class GuidAccountNumber : IGenerateAccountNumber
    {
        /* This method generates a unique account number using a GUID (Globally Unique Identifier).
         * It creates a new GUID and converts it to a byte array. Then, it takes the first 8 bytes of the
         * byte array and converts it to a long integer. The absolute value of this long integer is taken 
         * to ensure it's positive. Finally, the method returns the last 10 digits of this long integer 
         * as a string, formatted to be 10 digits long with leading zeros if necessary. */

        public string Generate()
        {
            long guidValue = Math.Abs(BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0));
            return (guidValue % 10000000000L).ToString("D10");
        }
    }
}