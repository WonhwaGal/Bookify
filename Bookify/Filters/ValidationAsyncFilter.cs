using Bookify.Models;
using Bookify.Models.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bookify.Filters
{
    public class ValidationAsyncFilter : IAsyncActionFilter
    {
        private readonly int _errorCode;

        public ValidationAsyncFilter(int errorCode)
        {
            _errorCode = errorCode;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(new ErrorResult
                {
                    ErrorCode = _errorCode,
                    ErrorMessages = context.ModelState
                            .Values
                            .Where(value => value.Errors.Any())
                            .SelectMany(value => value.Errors)
                            .Select(errorModel => errorModel.ErrorMessage)
                            .ToList()
                });
            }
            else
            {
                await next();
            }
        }
    }
}