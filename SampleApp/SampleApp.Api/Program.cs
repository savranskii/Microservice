using Microsoft.OpenApi.Models;
using SampleApp.Api.Extensions;
using SampleApp.Api.Infrastructure.Attributes;
using SampleApp.Api.Infrastructure.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom
        .Configuration(context.Configuration)
        .Enrich.FromLogContext());

builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureOptions();
builder.Services.ConfigureRateLimit(builder.Configuration);
builder.Services.ConfigureExceptionHandler();
builder.Services.ConfigureDependencies(builder.Configuration);

builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservice", Version = "v1", });
    c.OperationFilter<CustomHeaderSwaggerAttribute>();
});

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestContextLoggingMiddleware>();
app.UseExceptionHandler(opt => { });

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapCustomerEndpoints();

app.Run();

public partial class Program { }
