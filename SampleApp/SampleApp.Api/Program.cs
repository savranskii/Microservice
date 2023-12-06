using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Api.Application.Commands;
using SampleApp.Api.Application.Models;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.Models.Settings;
using SampleApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOptions<KafkaSettings>().BindConfiguration("Kafka").ValidateDataAnnotations().ValidateOnStart();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<Program>());   

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/api/customer/{email}", async (string email, [FromServices] IMediator mediator) => {
    return await mediator.Send(new GetCustomerByEmailQuery(email));
});

app.MapPost("/api/customer", async ([FromBody] CreateCustomerRequest data, [FromServices] IMediator mediator) => {
    return await mediator.Send(new CreateCustomerCommand(data.Email));
});

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
