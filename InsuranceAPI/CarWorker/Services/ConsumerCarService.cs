using CarWorker.Services.Interface;
using DataBaseModel.Data;
using DataBaseModel.Model;
using ModelLib.Constants;
using ModelLib.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CarWorker.Services;

public class ConsumerCarService : IConsumerCarService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string QueueName = RabbitConstants.CarInsuranceQueue;
    private IConfiguration _configuration;
    private readonly AppDbContext _ctx;


    public ConsumerCarService(AppDbContext ctx)
    {
        _ctx = ctx;
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, false)
            .Build();

        var factory = new ConnectionFactory
        {
            HostName = _configuration.GetValue<string>("RabbitMQ:Hostname"),
            UserName = _configuration.GetValue<string>("RabbitMQ:Username"),
            Password = _configuration.GetValue<string>("RabbitMQ:Password")
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    public Task StartConsuming(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (sender, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var model = JsonSerializer.Deserialize<CarPolicyDto>(message);

            await SaveToDatabase(model);

            _channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        };

        _channel.BasicConsume(QueueName, autoAck: false, consumer);

        return Task.CompletedTask;
    }


    private async Task SaveToDatabase(CarPolicyDto model)
    {
        var parcels = model.Values.Parcel;
        var amount = model.Values.Total / parcels;
        amount = Math.Round(amount, 2);
        var remnant = model.Values.Total - (amount * parcels);
        try
        {
            for (int count = 1; count <= parcels; count++)
            {
                remnant = count == 1 ? remnant : 0;
                var car = new CarParcelModel()
                {

                    Parcel = count,
                    Amount = amount + remnant,
                    MessageId = model.MessageId
                };
                _ctx.CarParcels.Add(car);
            }
            _ctx.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception("Error to save parcels" + ex.Message);
        }
    }
}
