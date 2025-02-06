namespace AntiFraudService.Worker.Extensions;


using MassTransit;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;


public static class ServiceConfigurationExtensions
{
    public static void UseSampleRetryConfiguration(this IKafkaTopicReceiveEndpointConfigurator configurator)
    {
        configurator.UseKillSwitch(k => k.SetActivationThreshold(1).SetRestartTimeout(m: 1).SetTripThreshold(0.2).SetTrackingPeriod(m: 1));
        configurator.UseMessageRetry(retry => retry.Interval(1000, TimeSpan.FromSeconds(1)));
    }

    public static T ConfigureSerilog<T>(this T builder)
        where T : IHostBuilder
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        builder.UseSerilog();

        return builder;
    }
}