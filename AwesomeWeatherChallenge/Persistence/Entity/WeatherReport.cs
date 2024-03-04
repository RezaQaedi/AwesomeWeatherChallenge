namespace AwesomeWeatherChallenge.Persistence.Entity;

public class WeatherReport
{
    public int Id { get; set; }
    public string Data { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
