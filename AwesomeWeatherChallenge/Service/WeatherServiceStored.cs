using AwesomeWeatherChallenge.Abstraction;

namespace AwesomeWeatherChallenge.Service;

public class WeatherServiceStored(IWeatherService inner, IRepository repository) : IWeatherService
{
    public async Task<string?> GetWeatherReportAsync(CancellationToken cs)
    {
        var report = await inner.GetWeatherReportAsync(cs);

        if (report is null)
        {
            return await repository.GetLastWeatherReportAsync();
        }
        else
        {
            await repository.AddAsync(report);
            return report;
        }
    }
}
