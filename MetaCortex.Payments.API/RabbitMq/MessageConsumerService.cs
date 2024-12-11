using System.Text;
using MetaCortex.Payments.DataAccess.Entities;
using MetaCortex.Payments.DataAccess.Interfaces;
using MetaCortex.Payments.DataAccess.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MetaCortex.Payments.API.RabbitMq;

public class MessageConsumerService : IMessageConsumerService
{
    private readonly IChannel _channel;
    private readonly IConnection _connection;
    private const string QueueName = "order-to-payment";
    private readonly ProcessConsumedOrderService _processedOrderService;
    private readonly IMessageProducerService _messageProducerService;

    public MessageConsumerService(IRabbitMqService rabbitMqService, IProcessedOrderRepository processedOrderRepository, IMessageProducerService messageProducerService)
    {
        _connection = rabbitMqService.CreateConnection().Result;
        _channel = _connection.CreateChannelAsync().Result;
        _channel.QueueDeclareAsync(QueueName, false, false, false).Wait();
        _processedOrderService = new ProcessConsumedOrderService(processedOrderRepository);
        _messageProducerService = messageProducerService;
    }

    public async Task ReadMessagesAsync()
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var payment = Encoding.UTF8.GetString(body);

            var processedPayment = await _processedOrderService.ProcessOrderAsync(payment);
            await _messageProducerService.SendPaymentToOrderAsync(processedPayment, "payment-to-order");
        };

        await _channel.BasicConsumeAsync(queue: "order-to-payment", autoAck: true, consumer: consumer);
        await Task.CompletedTask;
    }
}