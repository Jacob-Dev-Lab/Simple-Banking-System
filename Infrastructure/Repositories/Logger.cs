using SimpleBankingSystem.Application.Interfaces;

namespace SimpleBankingSystem.Infrastructure.Repositories
{
    public class Logger(string filePath) : ILogger
    {
        private readonly string _filePath = filePath;

        public void LogInfo(string message)
        {
            if (!File.Exists(_filePath))
                File.AppendAllText(_filePath, string.Empty);

            var logMessadge = $"{DateTime.UtcNow:f}: --> {message}{Environment.NewLine}";
            File.AppendAllText(_filePath, logMessadge);
        }
    }
}
