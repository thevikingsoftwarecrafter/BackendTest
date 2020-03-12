using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BackendTest.Infrastructure.CrossCutting.Logging
{
    public class LogContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogContextMiddleware> _logger;

        public LogContextMiddleware(RequestDelegate next, ILogger<LogContextMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public Task InvokeAsync(HttpContext context)
        {
            var correlationHeaders = Activity.Current.Baggage.Distinct().ToDictionary(b => b.Key, b => (object)b.Value);

            // ensures all entries are tagged with some values
            using (_logger.BeginScope(correlationHeaders))
            {
                // Call the next delegate/middleware in the pipeline
                return _next(context);
            }
        }
    }
}