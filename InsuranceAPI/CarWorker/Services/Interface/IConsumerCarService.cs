namespace CarWorker.Services.Interface;

public interface IConsumerCarService
{
    Task StartConsuming(CancellationToken stoppingToken);
}
