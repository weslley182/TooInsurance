namespace CarWorker.Services.Interface;

public interface IConsumerService
{
    Task StartConsuming(CancellationToken stoppingToken);        
}
