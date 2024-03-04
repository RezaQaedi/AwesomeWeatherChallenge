using AwesomeWeatherChallenge.Abstraction;
using AwesomeWeatherChallenge.Persistence.Entity;

namespace AwesomeWeatherChallenge.Service;

public class WeatherServiceStored(IWeatherService inner, IRepository repository) : IWeatherService
{
    public async Task<string?> GetWeatherReportAsync(CancellationToken cs)
    {
        var report = await inner.GetWeatherReportAsync(cs);

        if (report is null)
        {
            return (await repository.GetLastWeatherReportAsync(cs))?.Data;
        }
        else
        {
            await repository.AddAsync(new WeatherReport() { CreatedAt = DateTime.UtcNow, Data = report }, cs);
            return report;
        }
    }
}
