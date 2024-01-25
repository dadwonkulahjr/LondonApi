using LondonAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LondonAPI.Filters
{
    public class JsonExceptionFilter(IWebHostEnvironment webHostEnvironment) : IExceptionFilter
    {
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();

            if(_webHostEnvironment.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Details = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "A server error occured.";
                error.Details = context.Exception.Message;
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}
