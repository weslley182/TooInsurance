using DataBaseModel.Data;
using HomeWorker.Services;

namespace HomeWorker
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
            _logger.LogInformation("Start Home Service.");
            var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var consumer = new ConsumerHomeService(dbContext, _config);
            await consumer.StartConsuming(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Home Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(5000, stoppingToken);
            }

            _logger.LogInformation("End Service.");
        }
    }
}