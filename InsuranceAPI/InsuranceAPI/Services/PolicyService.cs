using ModelLib.Constants;
using ModelLib.Dtos;
using InsuranceAPI.Services.Interface;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        public async Task SendPolicy(PolicyDto policy)
        {
            try
            {                
                Send(policy);
            }
            catch (Exception e)
            {
                throw new Exception("Error to send queue: " + e.Message);
            }
        }        

        private void Send(PolicyDto policy)
        {
            var channel = CreateConfiguration();
            CreateQueueConfig(channel);

            var jsonString = JsonSerializer.Serialize(policy,
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    WriteIndented = true
                });

            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish(
                exchange: RabbitConstants.DefaultTooExchange,
                routingKey: policy.Product.ToString(),
                basicProperties: null,
                body: body);
        }

        private IModel CreateConfiguration()
        {
            var host = _configuration.GetValue<string>("RabbitMQ:Hostname");
            var factory = new ConnectionFactory() { HostName = host };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            return channel;
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
                routingKey: RabbitConstants.HomeInsuranceCod.ToString());

            channel.QueueBind(
                queue: RabbitConstants.CarInsuranceQueue,
                exchange: RabbitConstants.DefaultTooExchange,
                routingKey: RabbitConstants.CarInsuranceCod.ToString());
        }

    }
}
