using Microsoft.Extensions.Hosting;

namespace MetaCortex.Payments.DataAccess.RabbitMq;

public class MessageConsumerHostedService(IMessageConsumerService consumerService, IMessageProducerService producerService) : BackgroundService
{ 
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await consumerService.ReadMessagesAsync(); 
        Console.WriteLine("Consumer service is running");
    }
}