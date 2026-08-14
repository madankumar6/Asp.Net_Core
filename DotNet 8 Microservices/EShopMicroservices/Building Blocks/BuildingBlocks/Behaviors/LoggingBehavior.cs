using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] handling Request = {Request} - Response = {Response} - Request data = {RequestData}", 
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();

            try
            {
                return await next();
            }
            finally
            {
                timer.Stop();
                var timeTaken = timer.Elapsed;

                if (timeTaken.Seconds > 3)
                {
                    logger.LogWarning("[PERFORMANCE] The Request = {Request} took too long - Duration = {Duration} seconds",
                        typeof(TRequest).Name, timeTaken.Seconds);
                }

                logger.LogInformation("[END] handled Request = {Request} - Response = {Response} - Duration = {Duration}",
                    typeof(TRequest).Name, typeof(TResponse).Name, timeTaken.Seconds);
            }
        }
    }
}
