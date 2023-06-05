using CarWorker.Services;
using DataBaseModel.Data;

namespace CarWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;
        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IConfiguration cofig)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _config = cofig;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Start Service.");
            var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var consumer = new ConsumerCarService(dbContext, _config);
            await consumer.StartConsuming(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(5000, stoppingToken);
            }

            _logger.LogInformation("End Service.");
        }
    }
}