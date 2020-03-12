using System.Diagnostics;
using System.Threading;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BackendTest.Api.V1.Behaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Handling {{@Request}}", request);
            var stopWatch = Stopwatch.StartNew();
            var response = await next();
            stopWatch.Stop();
            _logger.LogInformation($"Handled {typeof(TRequest).Name}: {{@Response}} in {stopWatch.ElapsedMilliseconds}ms", response);
            return response;
        }
    }
}