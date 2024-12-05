using System.Text.Json;
using MetaCortex.Payments.DataAccess.Entities;
using MetaCortex.Payments.DataAccess.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace MetaCortex.Payments.API.RabbitMq;

public class ProcessConsumedOrderService
{
    private readonly IProcessedOrderRepository _processedOrderRepository;
    public ProcessConsumedOrderService(IProcessedOrderRepository processedOrderRepository)
    {
        _processedOrderRepository = processedOrderRepository;
    }
    public async Task<ProcessedOrder> ProcessOrderAsync(string order)
    {
        var orderDto = JsonSerializer.Deserialize<ProcessedOrder>(order);

        if (orderDto is not null)
        {
            var ProcessedOrder = new ProcessedOrder
            {
                OrderId = orderDto.OrderId,
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId,
                VIPStatus = orderDto.VIPStatus,
                Products = orderDto.Products,
                PaymentPlan = new Payment
                {
                    OrderId = orderDto.OrderId,
                    PaymentMethod = orderDto.PaymentMethod,
                    IsPaid = orderDto.IsPaid,
                }
            };
            await _processedOrderRepository.AddAsync(ProcessedOrder);
           
            return ProcessedOrder;
        }
        return null;
    }
}