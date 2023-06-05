namespace HomeWorker.Services.Interface
{
    internal interface IConsumerHomeService
    {
        Task StartConsuming(CancellationToken stoppingToken);
    }
}
