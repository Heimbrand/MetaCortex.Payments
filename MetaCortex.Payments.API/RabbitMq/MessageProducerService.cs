using System.Text;
using System.Text.Json;
using MetaCortex.Payments.DataAccess.Entities;
using MetaCortex.Payments.DataAccess.RabbitMq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RabbitMQ.Client;

namespace MetaCortex.Payments.API.RabbitMq;

public class MessageProducerService : IMessageProducerService
{
    private readonly ConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public MessageProducerService(RabbitMqConfiguration config)
    {
        _factory = new ConnectionFactory
        {
            HostName = config.HostName,
            UserName = config.Username,
            Password = config.Password
        };
        _connection = _factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;
    }

    public async Task SendPaymentToOrderAsync<T>(T order, string sendChannel)
    {
        await _channel.QueueDeclareAsync("payment-to-order", false, false, false);

        var json = JsonSerializer.Serialize(order);
        var body = Encoding.UTF8.GetBytes(json);

        await _channel.BasicPublishAsync("", sendChannel, body);

        Console.WriteLine($"Payment for order: {order} is sent back order");
    }
}