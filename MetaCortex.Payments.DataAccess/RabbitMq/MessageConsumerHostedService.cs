using MetaCortex.Payments.DataAccess.Entities;
using Microsoft.Extensions.Hosting;

namespace MetaCortex.Payments.DataAccess.RabbitMq;

public class MessageConsumerHostedService(IMessageConsumerService consumerService) : BackgroundService
{ 
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // För framtid utveckling kan det vara bra att ha en lista av queNames som hämtas från kön
        var queNames = new[] { "order-to-payment",};

        foreach (var que in queNames)
        {
            await consumerService.ReadMessagesAsync(que);
        }
        Console.WriteLine("Consumer service is running");
    }
}