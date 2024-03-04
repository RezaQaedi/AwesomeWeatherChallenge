using AwesomeWeatherChallenge.Abstraction;
using Quartz;

namespace AwesomeWeatherChallenge.Persistence;

internal class WeatherReportPruneJob(IRepository repository, ILogger<WeatherReportPruneJob> logger) : IJob
{
    public static readonly JobKey key = new("ReportPrune_Job", "HouseKeeping");

    public async Task Execute(IJobExecutionContext context)
    {
        if (context.RefireCount > 10)
        {
            logger.LogWarning("more than 10 try to run the job");
            return;
        }

        await repository.PruneAsync(context.CancellationToken);
    }
}