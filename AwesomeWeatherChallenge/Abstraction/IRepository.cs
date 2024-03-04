using AwesomeWeatherChallenge.Persistence.Entity;

namespace AwesomeWeatherChallenge.Abstraction;

public interface IRepository
{
    public Task<WeatherReport?> GetLastWeatherReportAsync(CancellationToken cs);
    public Task AddAsync(WeatherReport report, CancellationToken cs);
    public Task PruneAsync(CancellationToken cs);
}
