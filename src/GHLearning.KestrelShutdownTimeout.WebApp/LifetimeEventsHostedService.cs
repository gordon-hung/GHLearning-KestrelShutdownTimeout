using Microsoft.Extensions.Options;
using GHLearning.KestrelShutdownTimeout.WebApp;

namespace GHLearning.KestrelShutdownTimeout.WebApp;

internal class LifetimeEventsHostedService(
	ILogger<LifetimeEventsHostedService> logger,
	IHostApplicationLifetime appLifetime,
	GlobalSettings globalSettings,
	TimeProvider timeProvider) : IHostedService
{
	public Task StartAsync(CancellationToken cancellationToken)
	{
		appLifetime.ApplicationStarted.Register(OnStarted);
		appLifetime.ApplicationStopping.Register(OnStopping);
		appLifetime.ApplicationStopped.Register(OnStopped);

		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		try
		{
			logger.LogInformation("LogAt:{logAt} Method:{method} Message:The method has been invoked.", timeProvider.GetUtcNow(), nameof(StopAsync));
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "LogAt:{logAt} Method:{method} Message:Exception occurs.", timeProvider.GetUtcNow(), nameof(StopAsync));
		}

		return Task.CompletedTask;
	}

	private void OnStarted()
	{
		logger.LogInformation("LogAt:{logAt} Method:{method} Message:The method has been invoked.", timeProvider.GetUtcNow(), nameof(OnStarted));
	}

	private void OnStopping()
	{
		logger.LogInformation("LogAt:{logAt} Method:{method} Message:The method has been invoked.", timeProvider.GetUtcNow(), nameof(OnStopping));
		globalSettings.ShutdownTimeout = true;
	}

	private void OnStopped()
	{
		logger.LogInformation("LogAt:{logAt} Method:{method} Message:The method has been invoked.", timeProvider.GetUtcNow(), nameof(OnStopped));
	}
}
