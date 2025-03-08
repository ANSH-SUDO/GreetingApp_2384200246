using BusinessLayer.Interface;
using BusinessLayer.Service;
using HelloGreetingApplication.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Web;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System.Text;

// Logger to list logs in the debug window
var logger = LogManager.Setup().LoadConfigurationFromFile("NLog.config").GetCurrentClassLogger();
logger.Info("Starting application...");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

// Database Connection
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<GreetingContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IGreetingRL, GreetingRL>();
builder.Services.AddScoped<IGreetingBL, GreetingBL>();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure NLog
builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

// Swagger to list all apis
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

LogManager.Shutdown(); // Ensure logs are flushed
