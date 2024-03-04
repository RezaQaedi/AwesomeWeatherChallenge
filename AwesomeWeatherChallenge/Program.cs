using AwesomeWeatherChallenge.Abstraction;
using AwesomeWeatherChallenge.Persistence;
using AwesomeWeatherChallenge.Service;
using FluentMigrator.Runner;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentMigrator(builder.Configuration);
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddSingleton<IWeatherService, WeatherService>();
builder.Services.Decorate<IWeatherService, WeatherServiceStored>();
builder.Services.AddHttpClient<IWeatherService, WeatherService>(configureClient: c => c.Timeout = TimeSpan.FromSeconds(5));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// run MigrationRunner on startUp
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var runner = serviceScope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.Run();
