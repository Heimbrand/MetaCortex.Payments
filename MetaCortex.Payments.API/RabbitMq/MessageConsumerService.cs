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
    private readonly ILogger<MessageConsumerService> _logger;

    public MessageConsumerService(IRabbitMqService rabbitMqService, IProcessedOrderRepository processedOrderRepository, IMessageProducerService messageProducerService, ILogger<MessageConsumerService>logger)
    {
        _connection = rabbitMqService.CreateConnection().Result;
        _channel = _connection.CreateChannelAsync().Result;
        _channel.QueueDeclareAsync(QueueName, false, false, false).Wait();
        _processedOrderService = new ProcessConsumedOrderService(processedOrderRepository);
        _messageProducerService = messageProducerService;
        _logger = logger;
    }

    public async Task ReadMessagesAsync()
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var payment = Encoding.UTF8.GetString(body);
            _logger.LogInformation($"ORDER RECIEVED: {payment}");

            try
            {
                var processedPayment = await _processedOrderService.ProcessOrderAsync(payment);
                _logger.LogInformation($"ORDER PROCESSED: {processedPayment}");
                await _messageProducerService.SendPaymentToOrderAsync(processedPayment, "payment-to-order");
                _logger.LogInformation($"ORDER SENT BACK TO ORDER SERVICE:\n{processedPayment.Id},\n{processedPayment.PaymentPlan.PaymentMethod},\n{processedPayment.PaymentPlan.IsPaid},");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error processing order: {e.Message}");
            }
        };
        await _channel.BasicConsumeAsync(queue: "order-to-payment", autoAck: true, consumer: consumer);
        await Task.CompletedTask;
    }
}