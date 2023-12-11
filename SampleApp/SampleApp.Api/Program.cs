using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using SampleApp.Api.Infrastructure.ErrorHandlers;
using SampleApp.Api.Infrastructure.Extensions;
using SampleApp.Api.Infrastructure.Options;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.DbContexts;
using SampleApp.Infrastructure.Models.Settings;
using SampleApp.Infrastructure.Repositories;
using SampleApp.Infrastructure.Services;
using Serilog;

// TODO RateLimit

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Host.UseSerilog();

// Add services to the container.

var rateLimitOptions = new RateLimitOptions();
builder.Configuration.GetSection(RateLimitOptions.DefaultRateLimit).Bind(rateLimitOptions);
builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: RateLimitOptions.FixedPolicy, options =>
    {
        options.PermitLimit = rateLimitOptions.PermitLimit;
        options.Window = TimeSpan.FromSeconds(rateLimitOptions.Window);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = rateLimitOptions.QueueLimit;
    }));

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
