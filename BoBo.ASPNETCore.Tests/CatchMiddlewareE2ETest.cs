using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using BoBo.ASPNETCore.TestEndpoints;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BoBo.ASPNETCore.Tests;

public class CatchMiddlewareE2ETest
{
    [Fact]
    public async Task Middleware_Should_Return_Formatted_Exception()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });

        var response = await client.GetAsync("/Sample");

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.Equal("text/xml", response.Content.Headers.ContentType?.MediaType);

        var content = await response.Content.ReadAsStringAsync();
        var document = new XmlDocument();
        document.LoadXml(content);
        Assert.Equal("wow", document.SelectSingleNode("/Exception/Message")?.InnerText);
        Assert.Contains("such fun", document.SelectSingleNode("/Exception/InnerException/Message")?.InnerText);
    }
}
