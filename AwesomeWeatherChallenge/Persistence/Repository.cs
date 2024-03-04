namespace AwesomeWeatherChallenge.Abstraction;

internal class Repository(IConfiguration configuration) : IRepository
{
    public Task AddAsync(string report)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetLastWeatherReportAsync()
    {
        throw new NotImplementedException();
    }
}