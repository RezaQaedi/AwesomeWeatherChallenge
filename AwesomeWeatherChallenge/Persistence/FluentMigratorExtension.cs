using AwesomeWeatherChallenge.Persistence.Migrations;
using FluentMigrator.Runner;

namespace AwesomeWeatherChallenge.Persistence;

public static class FluentMigratorExtension
{
    public static IServiceCollection AddFluentMigrator(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(configuration.GetConnectionString("Application"))
                    .ScanIn(typeof(AddWeatherReports).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());
    }
}
