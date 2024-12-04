using MetaCortex.Payments.DataAccess.Entities;
using Microsoft.Extensions.Hosting;

namespace MetaCortex.Payments.DataAccess.RabbitMq;

public class MessageConsumerHostedService(IMessageConsumerService consumerService, IMessageProducerService producerService) : BackgroundService
{
    private Payment obj;
    private Payment obj2;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await consumerService.ReadMessagesAsync(); 
        //await producerService.SendPaymentToOrderAsync();
        Console.WriteLine("Consumer service is running");
    }
}