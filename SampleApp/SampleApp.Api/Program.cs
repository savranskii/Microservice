using SampleApp.Api.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region Logger

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Host.UseSerilog();

#endregion

// Add services to the container.

builder.Services.ConfigureOptions();
builder.Services.ConfigureRateLimit(builder.Configuration);
builder.Services.ConfigureExceptionHandler();
builder.Services.ConfigureDependencies(builder.Configuration);

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

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapCustomerEndpoints();

app.Run();
