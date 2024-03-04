using AwesomeWeatherChallenge.Abstraction;

namespace AwesomeWeatherChallenge;

internal class WeatherService(IHttpClientFactory clientFactory, ILogger<WeatherService> logger) : IWeatherService
{
    private const string Url = "forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m";

    public async Task<string?> GetWeatherReportAsync(CancellationToken cs)
    {
        var client = clientFactory.CreateClient("Forcast");

        try
        {
            var result = await client.GetAsync(Url, cancellationToken: cs);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsStringAsync(cs);
        }
        catch (Exception ex)
        {
            logger.LogError("that happened again! {errorMessage}", ex.Message);
        }

        return null;
    }
}