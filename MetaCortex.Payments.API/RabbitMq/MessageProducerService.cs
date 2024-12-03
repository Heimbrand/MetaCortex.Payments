using System.Text;
using System.Text.Json;
using MetaCortex.Payments.DataAccess.RabbitMq;
using RabbitMQ.Client;

namespace MetaCortex.Payments.API.RabbitMq;

public class MessageProducerService(RabbitMqConfiguration config) : IMessageProducerService
{
    private readonly ConnectionFactory _connectionFactory = new()
    {
        HostName = config.HostName,
        UserName = config.Username,
        Password = config.Password,
        VirtualHost = "/"
    };

    public async Task SendMessageAsync<T>(T payment)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(); 
        using var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync("payments", true, false, false);

        var jsonString = JsonSerializer.Serialize(payment);
        var body = Encoding.UTF8.GetBytes(jsonString);

        await channel.BasicPublishAsync(string.Empty, "payments",body);
    }
}