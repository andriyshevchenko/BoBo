using BoBo.ASPNETCore.Middleware;
using BoBo.Formatting.JSON;
using BoBo.Formatting.XML;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "BoBo.ASPNETCore.TestEndpoints", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BoBo.ASPNETCore.TestEndpoints v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware(typeof(Catch),
    System.Net.HttpStatusCode.InternalServerError,
    new WithContentType("text/xml"),
    new XmlDigest(
        new BoBo.Formatting.XML.RecursiveDump(
            new BoBo.Formatting.XML.XmlDump()
        )
    )
);

app.Run();

/// <summary>
/// Exposes the entry point class for integration tests.
/// </summary>
public partial class Program { }
