using Microsoft.EntityFrameworkCore;
using WeatherApp.Persistence;
using WeatherApp.Persistence.Context;
using WeatherApp.Services;
using WeatherApp.Services.Contract;
using WeatherApp.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();

builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
await dataContext?.Database.MigrateAsync();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.MapControllers();
app.Run();