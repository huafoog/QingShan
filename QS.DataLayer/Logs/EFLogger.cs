using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace QS.DataLayer.Logs
{
    public class EFLogger : ILogger
    {
        private readonly string categoryName;

        public EFLogger(string categoryName) => this.categoryName = categoryName;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {

            if (categoryName == DbLoggerCategory.Database.Command.Name &&
            logLevel == LogLevel.Information)
            {
                var logContent = formatter(state, exception);

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(logContent);
                Console.ResetColor();
            }
        }

        public IDisposable BeginScope<TState>(TState state) => null;
    }
}
