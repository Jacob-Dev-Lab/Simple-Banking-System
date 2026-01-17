using SimpleBankingSystem.Interfaces;

namespace SimpleBankingSystem.Utilities
{
    class GuidAccountNumber : IGenerateAccountNumber
    {
        public string Generate()
        {
            long guidValue = Math.Abs(BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0));
            return (guidValue % 10000000000L).ToString("D10");
        }
    }
}