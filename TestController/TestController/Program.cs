using Microsoft.AspNetCore.Builder;
using TestController;
using Serilog.Events;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var appName = typeof(Program).Assembly.GetName().Name;

var loggerConfiguration = new LoggerConfiguration();

Log.Logger = loggerConfiguration.MinimumLevel.Debug()
								.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
								.Enrich.FromLogContext()
								.WriteTo.Console()
								.CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<MessageHub>("/hub");

app.Run();
