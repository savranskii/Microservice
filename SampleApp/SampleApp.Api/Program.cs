using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Api.Application.Commands;
using SampleApp.Api.Application.Models;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.DomainEvents;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.Models.Settings;
using SampleApp.Infrastructure.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Host.UseSerilog();

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

app.MapGet("/api/customer/{email}", async (string email, [FromServices] IMediator mediator, [FromServices] ILogger<Program> logger) =>
{
    logger.LogInformation("Execute get customer");

    return await mediator.Send(new GetCustomerByEmailQuery(email));
})
.WithName("GetCustomer")
.WithOpenApi();

app.MapPost("/api/customer", async ([FromBody] CreateCustomerRequest data, [FromServices] IMediator mediator, [FromServices] ILogger<Program> logger) =>
{
    logger.LogInformation("Execute get customer");

    var customer = await mediator.Send(new CreateCustomerCommand(data.Email));

    await mediator.Publish(new CustomerCreatedDomainEvent(1, "debit", "123", "John Doe", DateTime.UtcNow));

    return customer;
})
.WithName("CreateCustomer")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
