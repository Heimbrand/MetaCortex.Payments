﻿using System.Text;
using MetaCortex.Payments.API.Extensions;
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
    private readonly ProcessConsumedOrderService _processedOrderService;
    private readonly IMessageProducerService _messageProducerService;
    private readonly ILogger<MessageConsumerService> _logger;
    private readonly IProcessedPaymentHistoryRepository _processedPaymentHistoryRepository;

    public MessageConsumerService(IRabbitMqService rabbitMqService, IProcessedPaymentHistoryRepository processedPaymentHistoryRepository, IMessageProducerService messageProducerService, ILogger<MessageConsumerService> logger)
    {
        _connection = rabbitMqService.CreateConnection().Result;
        _channel = _connection.CreateChannelAsync().Result;
        _processedOrderService = new ProcessConsumedOrderService(processedPaymentHistoryRepository);
        _messageProducerService = messageProducerService;
        _logger = logger;
        _processedPaymentHistoryRepository = processedPaymentHistoryRepository;
    }

    public async Task ReadMessagesAsync(string quename)
    {
        _channel.QueueDeclareAsync(quename, false, false, false).Wait();
        var consumer = new AsyncEventingBasicConsumer(_channel);

        if (quename == "order-to-payment")
        {
            consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var payment = Encoding.UTF8.GetString(body);
                    _logger.LogInformation($"ORDER RECIEVED: {payment}");

                    try
                    {
                        var processedPayment = await _processedOrderService.ProcessOrderAsync(payment);
                        _logger.LogInformation($"ORDER PROCESSED: {processedPayment}");

                        var newPaymentHistory = new PaymentHistory
                        {
                            OrderId = processedPayment?.Id,
                            PaymentMethod = processedPayment?.PaymentPlan?.PaymentMethod,
                            IsPaid = processedPayment?.PaymentPlan?.IsPaid,
                            PaymentDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day)
                        };

                        foreach (var product in processedPayment.Products)
                        {
                            newPaymentHistory.Products.Add(new Products
                            {
                                id = product.id,
                                Name = product.Name,
                                Price = product.Price,
                                Quantity = product.Quantity
                            });
                        }

                        await ProcessedOrderEndpointExtensions.AddPaymentAsync(_processedPaymentHistoryRepository, newPaymentHistory);
                        _logger.LogInformation($"ORDER SAVED TO DATABASE: {newPaymentHistory}");

                        await _messageProducerService.SendPaymentToOrderAsync(processedPayment, "payment-to-order");
                        _logger.LogInformation($"ORDER SENT BACK TO ORDER SERVICE:\n{processedPayment?.Id},\n{processedPayment?.PaymentPlan?.PaymentMethod},\n{processedPayment?.PaymentPlan?.IsPaid},");
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
}