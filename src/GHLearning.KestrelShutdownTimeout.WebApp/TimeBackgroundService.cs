using GHLearning.KestrelShutdownTimeout.WebApp;

namespace GHLearning.KestrelShutdownTimeout.WebApp;

public class TimeBackgroundService(
	ILogger<TimeBackgroundService> logger,
	TimeProvider timeProvider,
	GlobalSettings globalSettings) : BackgroundService
{
	private readonly int _seconds = 35;
	private int _count = 0;

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!globalSettings.ShutdownTimeout)
		{
			try
			{
				_count++;
				logger.LogInformation("LogAt:{logAt} Count:{count} Message:This is being executed.", timeProvider.GetUtcNow(), _count);
				await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken).ConfigureAwait(false);
			}
			catch (TaskCanceledException ex)
			{
				logger.LogError(ex, "LogAt:{logAt} Count:{count} Message:{message}.", timeProvider.GetUtcNow(), _count, ex.Message);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "LogAt:{logAt} Count:{count} Message:{message}.", timeProvider.GetUtcNow(), _count, ex.Message);
			}
		}

		using var cancellationTokenSource = new CancellationTokenSource();
		logger.LogInformation("LogAt:{logAt} Message:Begin at {seconds} seconds.", timeProvider.GetUtcNow(), _seconds);
		await Task.Delay(TimeSpan.FromSeconds(_seconds), cancellationTokenSource.Token).ConfigureAwait(false);
		logger.LogInformation("LogAt:{logAt} Message:Finish at {seconds} seconds.", timeProvider.GetUtcNow(), _seconds);
	}
}
