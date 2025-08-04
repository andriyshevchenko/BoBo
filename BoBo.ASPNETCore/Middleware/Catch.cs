using BoBo.Formatting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BoBo.ASPNETCore.Middleware;

/// <summary>
/// Bridge between ASP.NET framework and "elegant objects".
/// </summary>
public class Catch
{
    private readonly RequestDelegate next;
    private readonly HttpStatusCode code;
    private readonly IHeaders headers;
    private readonly IDigest digest;

    /// <summary>
    /// Initializes a new instance of the <see cref="Catch"/> middleware.
    /// </summary>
    /// <param name="next">The next component in the pipeline.</param>
    /// <param name="code">HTTP status code to return when an exception is caught.</param>
    /// <param name="headers">Headers to append to the response.</param>
    /// <param name="digest">Algorithm used to serialize exception details.</param>
    public Catch(RequestDelegate next, HttpStatusCode code, IHeaders headers, IDigest digest)
    {
        this.next = next;
        this.code = code;
        this.headers = headers;
        this.digest = digest;
    }

    /// <summary>
    /// Executes the next component and captures any unhandled exceptions.
    /// </summary>
    /// <param name="httpContext">Current HTTP context.</param>
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
            var control = httpContext.Features.Get<IHttpBodyControlFeature>();
            if (control != null)
            {
                control.AllowSynchronousIO = true;
            }
            await digest.Write(exception, httpContext.Response.Body);
        }
    }
}
