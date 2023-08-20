using HR.LeaveManagement.Application.Exceptions;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using System.Net;

namespace HR.LeaveManagement.api.MIddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            { 
                await HandleAwaitException(httpContext, ex);
            }
        }

        private Task HandleAwaitException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string result = JsonConvert.SerializeObject(new ErrorDetails
            {  ErrorMessage =ex.Message, 
             ErrorType = "Failure" });

            switch (ex)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject( validationException.Errors);
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                default:
                    break;
            };

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
         }
    }
    public class ErrorDetails
    {
        public string ErrorType { get; set; } = null!;
        public string ErrorMessage { get; set; } = null!;
    }

}
