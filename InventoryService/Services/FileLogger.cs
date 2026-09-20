using Microsoft.Extensions.Logging;

namespace InventoryService.Services
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;
        private readonly object _lock = new();

        public FileLogger(string filePath)
        {
            _filePath = filePath;

            var directory = Path.GetDirectoryName(_filePath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var message = formatter(state, exception);

            if (exception != null)
            {
                message += Environment.NewLine + exception;
            }

            var logMessage =
                $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} " +
                $"[{logLevel}] {message}";

            lock (_lock)
            {
                File.AppendAllText(
                    _filePath,
                    logMessage + Environment.NewLine);
            }
        }
    }
}