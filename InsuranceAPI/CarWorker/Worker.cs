using CarWorker.Services;
using CarWorker.Services.Interface;
using DataBaseModel.Data;
using DataBaseModel.Repository.Interface;

namespace CarWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ConsumerService _consumerService;
        private readonly ICarInsuranceRepository _repo;
        private readonly AppDbContext _dbContext;

        //public Worker(ILogger<Worker> logger, AppDbContext dbContext)
        public Worker(ILogger<Worker> logger, ICarInsuranceRepository repo)
        {
            //_dbContext = dbContext;
            _logger = logger;
            _repo = repo;
            //_consumerService = rabbitMQConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Start Service.");

            var consumer = new ConsumerService(_repo);
            consumer.StartConsuming(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(5000, stoppingToken);
            }

            _logger.LogInformation("End Service.");
        }        
    }
}