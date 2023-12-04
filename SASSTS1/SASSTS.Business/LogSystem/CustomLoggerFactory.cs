using Infrastructure.Models;
using Microsoft.Extensions.Logging;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SASSTS.Business.LogSystem
{
    public class FileLogger : ActionFilterAttribute, ILogger, IAuthorizationFilter
    {
        private readonly string _filePath;
        private static object _lockObject = new object();
        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public IDisposable? BeginScope<TState>(TState state) => null;

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string logMsg = formatter(state, exception);

            logMsg = $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] - {logMsg}";

            // Kilit mekanizması ekleyerek çoklu işlemler arasında dosyaya yazma işleminin güvenliğini sağlayabilirsiniz.
            lock (_lockObject)
            {
                using (var writer = new StreamWriter(_filePath, true)) // true: Varolan dosyanın üzerine yaz
                {
                    writer.WriteLine(logMsg);
                    writer.Close();
                }
            }
        }
    }
    public class CustomLoggerFactory : ActionFilterAttribute, ILoggerProvider, IAuthorizationFilter
    {
        private readonly string _filePath;

        public CustomLoggerFactory(string filePath)
        {
            _filePath = filePath;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_filePath);
        }

        public void Dispose()
        {
        }

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            throw new NotImplementedException();
        }
    }
}
