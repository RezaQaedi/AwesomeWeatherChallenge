using AwesomeWeatherChallenge.Persistence.Entity;
using Dapper;

namespace AwesomeWeatherChallenge.Abstraction;

internal class Repository(IDbConnetionFactory connetionFactory) : IRepository
{
    public async Task AddAsync(WeatherReport report, CancellationToken cs)
    {
        using var connetion = connetionFactory.CreateConnetion();
        // return result to caller ? 
        var result = await connetion.ExecuteAsync(new CommandDefinition("INSERT INTO Reports ([Data], [CreatedAt]) Values (@Data, @CreatedAt)", report, cancellationToken: cs));
    }

    public async Task<WeatherReport?> GetLastWeatherReportAsync(CancellationToken cs)
    {
        using var connetion = connetionFactory.CreateConnetion();
        return await connetion.QuerySingleOrDefaultAsync<WeatherReport>(
            new CommandDefinition("SELECT TOP 1 * FROM Reports ORDER BY CreatedAt DESC", cancellationToken: cs));
    }
}