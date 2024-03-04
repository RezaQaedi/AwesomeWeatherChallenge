using Quartz;

namespace AwesomeWeatherChallenge.Persistence;

public static class QuartzExtension
{
    public static IServiceCollection RegisterQuartz(this IServiceCollection services)
    {
        services.AddQuartz(c =>
        {
            c.AddJob<WeatherReportPruneJob>(WeatherReportPruneJob.key, p => p.WithDescription("Prune not needed reports from data base"));

            c.AddTrigger(p =>
                p.WithIdentity("ReportPrune_trigger")
                .ForJob(WeatherReportPruneJob.key)
                .StartNow()
                .WithCronSchedule("0 0 0 15 * ? *")
                .WithDescription("At 00:00:00am, on the 15th day, every month"));
        });

        services.AddQuartzHostedService(o => o.WaitForJobsToComplete = true);

        return services;
    }
}
