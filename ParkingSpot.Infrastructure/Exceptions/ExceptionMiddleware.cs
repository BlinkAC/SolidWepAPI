using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ParkingSpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.Exceptions
{
    public sealed class ExceptionMiddleware : IMiddleware
    {
        public ILogger<ExceptionMiddleware> _logger { get; }
        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await HandleExceptionsAsync(exception, context);
            }
        }

        private async Task HandleExceptionsAsync(Exception exception, HttpContext context)
        {
            var (statusCode, error) = exception switch
            {
                CustomException => (StatusCodes.Status400BadRequest, new Error(exception.GetType().Name, exception.Message)),
               _  => (StatusCodes.Status500InternalServerError, new Error("error","There's was an error"))
            };

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(error);
        }

        private record Error(string code, string reason)
        {

        }
    }
}
