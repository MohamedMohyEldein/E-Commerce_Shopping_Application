using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Presentation.Middlewares
{
    public class ExceptionHandlingMiddleware : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var code = HttpStatusCode.InternalServerError;
            switch (exception)
            {
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case BadRequestException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedException:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case ConflictException:
                    code = HttpStatusCode.Conflict;
                    break;
                case KeyNotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case ArgumentNullException:
                case ArgumentException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedAccessException:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }



            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;
            await httpContext.Response.WriteAsJsonAsync(new
            {
                title = "An error occured",
                status = (int)code,
                detail = exception.Message
            });

            return true;
        }
    }
}
