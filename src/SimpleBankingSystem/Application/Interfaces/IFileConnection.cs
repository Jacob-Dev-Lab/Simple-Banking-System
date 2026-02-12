namespace SimpleBankingSystem.Application.Interfaces
{
    public interface IFileConnection
    {
        public string CustomerFilePath { get; }
        public string AccountFilePath { get; }
        public string TransactionFilePath { get; }
        public string LogFilePath { get; }
    }
}
