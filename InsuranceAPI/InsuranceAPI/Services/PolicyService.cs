using InsuranceAPI.Constants;
using InsuranceAPI.Dto;
using InsuranceAPI.Services.Interface;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;


namespace InsuranceAPI.Services
{
    public class PolicyService: IPolicyService
    {
        private IConfiguration _configuration;

        public PolicyService()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, false)
                .Build();
        }
        public async Task SendPolicy(Policy policy)
        {
            try
            {                
                Send(policy);
            }
            catch (Exception e)
            {
                throw new Exception("Error: " + e.Message);
            }
        }        

        private void Send(Policy policy)
        {            
            var host = _configuration.GetValue<string>("RabbitMQ:Hostname");
            var factory = new ConnectionFactory() { HostName = host };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            CreateQueueConfig(channel);

            string jsonString = JsonSerializer.Serialize(policy);
            var body = Encoding.UTF8.GetBytes(jsonString);
            
            channel.BasicPublish(
                exchange: RabbitConstants.DefaultTooExchange,
                routingKey: policy.Product.ToString(),
                basicProperties: null,
                body: body);
        }

        private void CreateQueueConfig(IModel channel)
        {            
            channel.ExchangeDeclare(
                exchange: RabbitConstants.DefaultTooExchange, 
                type: ExchangeType.Direct);
            
            channel.QueueDeclare(
                queue: RabbitConstants.HomeInsuranceQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueDeclare(
                queue: RabbitConstants.CarInsuranceQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);            

            channel.QueueBind(
                queue: RabbitConstants.HomeInsuranceQueue,
                exchange: RabbitConstants.DefaultTooExchange,
                routingKey: RabbitConstants.HomeInsuranceCod);

            channel.QueueBind(
                queue: RabbitConstants.CarInsuranceQueue,
                exchange: RabbitConstants.DefaultTooExchange,
                routingKey: RabbitConstants.CarInsuranceCod);
        }

    }
}
