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

    public async Task PruneAsync(CancellationToken cs)
    {
        using var connection = connetionFactory.CreateConnetion();
        await connection.ExecuteAsync(new CommandDefinition("DELETE Reports WHERE CAST(Reports.CreatedAt AS DATE) < CAST(DATEADD(DAY, -15, GETUTCDATE()) AS DATE)", cancellationToken: cs));
    }
}