using System.Text.Json;
using MetaCortex.Payments.DataAccess.Entities;
using MetaCortex.Payments.DataAccess.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace MetaCortex.Payments.API.RabbitMq;

public class ProcessConsumedOrderService
{
    private readonly IPaymentRepository _paymentRepository;
    public ProcessConsumedOrderService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }
    public async Task<Payment> ProcessOrderAsync(string order)
    {
        var orderDto = JsonSerializer.Deserialize<OrderDto>(order);

        if (orderDto is not null)
        {
            var payment = new Payment
            {
                OrderId = orderDto.OrderId,
                PaymentMethod = orderDto.PaymentMethod,
                IsPaid = true,
            };
            await _paymentRepository.AddAsync(payment);
           
            return payment;
        }
        return null;
    }
}