using AwesomeWeatherChallenge.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeWeatherChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController(IWeatherService weatherService) : ControllerBase
{
    [HttpGet("getWeatherReport")]
    public async Task<IActionResult> GetWeatherReport(CancellationToken cs)
    {
        var result = await weatherService.GetWeatherReportAsync(cs);

        // set appropriate error message ? 
        if (result == null)
            return Problem(statusCode: 500);

        return Ok(result);
    }
}
