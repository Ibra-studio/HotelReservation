namespace HotelReservation.API.Middleware
{
        public class ExceptionMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger<ExceptionMiddleware> _logger; //permet de logger les erreurs dans vs

            public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
            {
                _next = next;
                _logger = logger;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (KeyNotFoundException ex)
                {
                    _logger.LogWarning(ex, "KeyNotFoundException capturée");
                    await HandleExceptionAsync(context, 404, ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    _logger.LogWarning(ex, "InvalidOperationException capturée");
                    await HandleExceptionAsync(context, 400, ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception non gérée");
                    await HandleExceptionAsync(context, 500, ex.Message);
                }
            }

            private static Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
            {
                if (context.Response.HasStarted)  // sans ça si asp.net à envoyer la reponse avant que l'exception soit attrapee , on ne peut plus modifier le status code
                {
                    return Task.CompletedTask;
                }

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsJsonAsync(new { message });
            }
        }
    }


