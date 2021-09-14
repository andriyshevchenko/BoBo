using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BoBo.Formatting;

namespace BoBo.EndpointFilter
{
    public class JsonFootprintAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = 
                new ContentResult()
                {
                    Content = new JsonDetails(
                        context.Exception,
                        new JsonFootprint(context.Exception)
                    ).MakeFootprint().ToString(),
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            context.ExceptionHandled = true;
        }
    }
}