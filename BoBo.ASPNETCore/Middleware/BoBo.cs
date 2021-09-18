using BoBo.Formatting;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BoBo.ASPNETCore.Middleware;

/// <summary>
/// Bridge between ASP.NET framework and "elegant objects"
/// </summary>
public class BoBo
{
    private readonly RequestDelegate next;
    private readonly HttpStatusCode code;
    private readonly IHeaders headers;
    private readonly IDigest digest;

    public BoBo(RequestDelegate next, HttpStatusCode code, IHeaders headers, IDigest digest)
    {
        this.next = next;
        this.code = code;
        this.headers = headers;
        this.digest = digest;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception exception)
        {
            httpContext.Response.StatusCode = (int)code;
            foreach (var item in headers.Make())
            {
                httpContext.Response.Headers.Add(item.Key, item.Value);
            }
            await digest.Write(exception, httpContext.Response.Body);
        }
    }
}
