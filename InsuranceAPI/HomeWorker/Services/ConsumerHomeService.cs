﻿using DataBaseModel.Data;
using DataBaseModel.Model;
using HomeWorker.Services.Interface;
using ModelLib.Constants;
using ModelLib.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace HomeWorker.Services;

public class ConsumerHomeService : IConsumerHomeService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string QueueName = RabbitConstants.HomeInsuranceQueue;
    private IConfiguration _configuration;
    private readonly AppDbContext _ctx;


    public ConsumerHomeService(AppDbContext ctx, IConfiguration config)
    {
        _ctx = ctx;
        _configuration = config;

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
            var model = JsonSerializer.Deserialize<HomePolicyDto>(message);

            await SaveToDatabase(model);

            _channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        };

        _channel.BasicConsume(QueueName, autoAck: false, consumer);

        return Task.CompletedTask;
    }


    private async Task SaveToDatabase(HomePolicyDto model)
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
                var home = new HomeParcelModel()
                {

                    Parcel = count,
                    Amount = amount + remnant,
                    MessageId = model.MessageId
                };
                _ctx.HomeParcels.Add(home);
            }
            _ctx.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception("Error to save home parcels" + ex.Message);
        }
    }
}
