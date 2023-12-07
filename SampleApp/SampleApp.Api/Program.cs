using Microsoft.EntityFrameworkCore;
using SampleApp.Api.Infrastructure.Endpoints;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.DbContexts;
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
builder.Services.AddDbContext<CustomerContext>(opt => opt.UseInMemoryDatabase("CustomerDb"));

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

app.MapCustomerEndpoints();

app.Run();
