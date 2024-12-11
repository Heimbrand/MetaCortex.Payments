using MetaCortex.Payments.DataAccess.Entities;
using Microsoft.Extensions.Hosting;

namespace MetaCortex.Payments.DataAccess.RabbitMq;

public class MessageConsumerHostedService(IMessageConsumerService consumerService, IMessageProducerService producerService) : BackgroundService
{ 
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        var queNames = new[] { "order-to-payment",};

        foreach (var que in queNames)
        {
            await consumerService.ReadMessagesAsync(que);
        }
        Console.WriteLine("Consumer service is running");
    }
}