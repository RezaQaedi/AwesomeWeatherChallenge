namespace AwesomeWeatherChallenge.Abstraction;

public interface IWeatherService
{
    public Task<string?> GetWeatherReportAsync(CancellationToken cs);
}
