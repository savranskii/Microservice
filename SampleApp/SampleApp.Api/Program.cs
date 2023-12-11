using Microsoft.EntityFrameworkCore;
using SampleApp.Api.Infrastructure.ErrorHandlers;
using SampleApp.Api.Infrastructure.Extensions;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.DbContexts;
using SampleApp.Infrastructure.Models.Settings;
using SampleApp.Infrastructure.Repositories;
using SampleApp.Infrastructure.Services;
using Serilog;

// TODO RateLimit
// TODO hide fields from models

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddExceptionHandler<DefaultExceptionHandler>();

builder.Services.AddDbContext<CustomerContext>(opt => opt.UseInMemoryDatabase("CustomerDb"));

builder.Services.AddOptions<KafkaSettings>().BindConfiguration("Kafka").ValidateDataAnnotations().ValidateOnStart();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapCustomerEndpoints();

app.Run();
