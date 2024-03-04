using AwesomeWeatherChallenge;
using AwesomeWeatherChallenge.Abstraction;
using AwesomeWeatherChallenge.Persistence;
using AwesomeWeatherChallenge.Service;
using FluentMigrator.Runner;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentMigrator(builder.Configuration);
builder.Services.AddSingleton<IDbConnetionFactory, DbConnetionFactory>(_ => new DbConnetionFactory(builder.Configuration.GetConnectionString("Application")!));
builder.Services.AddSingleton<IRepository, Repository>();
builder.Services.AddHttpClient("Forcast", client =>
{
    client.BaseAddress = new Uri("https://api.open-meteo.com/v1/");
    client.Timeout = TimeSpan.FromSeconds(5);
});
builder.Services.AddSingleton<IWeatherService, WeatherService>();
builder.Services.Decorate<IWeatherService, WeatherServiceStored>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// run MigrationRunner on start-up
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var runner = serviceScope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.Run();
