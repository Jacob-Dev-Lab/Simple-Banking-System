using SimpleBankingSystem.Application.Interfaces;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    public class Logger(IFileConnection connection) : ILogger
    {
        private readonly string _filePath = connection.LogFilePath;

        /*  The LogInfo method is responsible for logging informational messages to a file. 
         *  It first checks if the log file exists, and if it doesn't, it creates an empty file. 
         *  Then, it constructs a log message by combining the current UTC date and time with 
         *  the provided message. Finally, it appends the log message to the log file. This method 
         *  allows for tracking important events or information in the application by writing 
         *  them to a log file. */

        public void LogInfo(string message)
        {
            if (!File.Exists(_filePath))
                File.AppendAllText(_filePath, string.Empty);

            var logMessadge = $"{DateTime.UtcNow:f}: --> {message}{Environment.NewLine}";
            File.AppendAllText(_filePath, logMessadge);
        }
    }
}
