using Application.DTO.Response ;
using Application.Exceptions.PostExceptions;
using Application.Exceptions.UserExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class GlobalExceptions : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (var status, var message) = exception switch
            {
                UserNotFoundException userNotFoundException => (404, userNotFoundException.Message),
                UserExistsException userExistsException => (409, userExistsException.Message),
                InvalidUserCredentials invalidUserCredentials => (401, invalidUserCredentials.Message),
                PostNotFoundException postNotFoundException => (404, postNotFoundException.Message),
                _ => (500, "Error"),
            };

            httpContext.Response.StatusCode = status;

            var errorResponse = new ErrorResponse()
            {
                StatusCode = status,
                Message = message
            };

            await httpContext.Response.WriteAsJsonAsync(errorResponse);

            return true;
        }
    }
}
