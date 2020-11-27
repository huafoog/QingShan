using Microsoft.Extensions.Logging;

namespace QS.DataLayer.Logs
{
    public class EFLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new EFLogger(categoryName);
        }

        public void Dispose() { }
    }
}
