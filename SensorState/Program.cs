using Microsoft.EntityFrameworkCore;
using SensorState.Context;
using SensorState.Middleware;
using Serilog;

// Build a configuration object using JSON provider and environment variables.
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure logging provider
var logger = new LoggerConfiguration()
             .Enrich.FromLogContext()
             .Enrich.WithProcessId()
             .Enrich.WithMachineName()
             .Enrich.WithEnvironmentUserName()
             .ReadFrom.Configuration(configuration)
             .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
