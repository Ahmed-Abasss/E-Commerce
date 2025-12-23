using E_Commerce.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.CustomMiddlewares
{
    public class ExceptionHandlerMiddleware
    {
        
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next , ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                await NotFoundEndPoint(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "something went wrong");

                var prop = new ProblemDetails()
                {
                    Title = "Error While Processing Http Request ",
                    Detail = ex.Message,
                    Instance = context.Request.Path,
                    Status = ex switch
                    {
                        NotFoundException=>StatusCodes.Status404NotFound,
                        _=>StatusCodes.Status500InternalServerError
                    }
                };
                context.Response.StatusCode = prop.Status.Value;

                await context.Response.WriteAsJsonAsync(prop);
            }
        }

        private static async Task NotFoundEndPoint(HttpContext context )
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound && !context.Response.HasStarted)
            {
                var response = new ProblemDetails()
                {
                    Title = "Error While Processing Http Requset - EndPoint Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"EndPoint {context.Request.Path} NotFound",
                    Instance = context.Request.Path
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
