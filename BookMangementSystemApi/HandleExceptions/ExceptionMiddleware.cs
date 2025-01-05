namespace BookMangementSystemApi.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API Exception: {ex.Message}");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.StatusCode;

                await context.Response.WriteAsJsonAsync(new
                {
                    statusCode = ex.StatusCode,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled Exception: {ex.Message}");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(new
                {
                    statusCode = 500,
                    message = "An unexpected error occurred. Please try again later."
                });
            }
        }
    }

}
