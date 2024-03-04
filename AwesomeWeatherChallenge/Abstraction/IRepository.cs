namespace AwesomeWeatherChallenge.Abstraction;

public interface IRepository
{
    public Task<string?> GetLastWeatherReportAsync();
    public Task AddAsync(string report);
}
