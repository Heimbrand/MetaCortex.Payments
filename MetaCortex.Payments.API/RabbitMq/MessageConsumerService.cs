using System.Text;
using MetaCortex.Payments.DataAccess.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MetaCortex.Payments.API.RabbitMq;

public class MessageConsumerService : IMessageConsumerService
{
    public async Task ReadMessagesAsync()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
        };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();


        await channel.QueueDeclareAsync("payments", true, false, false);
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Message: {message}");
        };
        await channel.BasicConsumeAsync("payments", true, consumer);
    }
}