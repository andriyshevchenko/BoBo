using BoBo.Decorating;
using BoBo.Formatting;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace BoBo.ASPNETCore.Middleware
{
    public class BoBo
    {
        private readonly RequestDelegate next;
        private readonly IFootprint footprint;

        public BoBo(RequestDelegate next, IFootprint footprint)
        {
            this.next = next;
            this.footprint = footprint;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception exception)
            {
                string body = 
                    new TextOf(
                        new JsonDetails(
                        exception,
                        footprint
                    )
                ).Text();
                httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(body);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync(body);  
            }
        }
    }
}
