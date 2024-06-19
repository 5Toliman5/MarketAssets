using MarketAssets.Domain.Exceptions;
using Newtonsoft.Json;

namespace MarketAssets.Api.Middleware
{
    public class ErrorHandler
    {
        private readonly RequestDelegate Next;
        private readonly ILogger<ErrorHandler> Logger;

        public ErrorHandler(RequestDelegate next, ILogger<ErrorHandler> logger)
        {
            this.Next = next;
            this.Logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.Next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (ex)
                {
                    case NotFoundException:
                        // not found error
                        this.Logger.LogError(default(EventId), ex.Message);
                        response.StatusCode = StatusCodes.Status404NotFound;
                        break;
                    case TimeoutException:
                        // timeout error
                        this.Logger.LogError(default(EventId), ex.Message);
                        response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                        break;
                    default:
                        this.Logger.LogCritical(ex, "Critical error");
                        // unhandled error
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }

                var error = new { error = ex?.Message };
                var result = JsonConvert.SerializeObject(error);
                await response.WriteAsync(result);
            }
        }
    }
}
