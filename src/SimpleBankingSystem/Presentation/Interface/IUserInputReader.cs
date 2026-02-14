namespace SimpleBankingSystem.Presentation.Interface
{
    internal interface IUserInputReader
    {
        public string ReadString(string prompt);

        public DateOnly ReadDateOfBirth(string prompt);

        public string? ReadEmailString(string prompt);

        public int ReadInt(string prompt);

        public string ReadAccountNumber(string prompt);

        public decimal ReadDecimal(string prompt);

        public (string lastName, string otherNames, DateOnly dateOfBirth, string? email) ReadCustomer();
    }
}
