using BoBo.Fakes;
using BoBo.Formatting;

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

app.Use(async (context, next) =>
{
    await new BoBo.ASPNETCore.Middleware.BoBo(next, new JsonFootprint()).InvokeAsync(context);
});

app.Run();
