using FluentValidation;

namespace OrderManagement.Api.Middleware
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException exception)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors = exception.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(error => error.ErrorMessage).ToArray());

                await context.Response.WriteAsJsonAsync(new
                {
                    title = "One or more validation errors occurred.",
                    status = StatusCodes.Status400BadRequest,
                    errors
                });
            }
        }
    }
}
