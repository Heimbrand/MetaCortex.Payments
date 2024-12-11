using System.Text.Json;
using MetaCortex.Payments.API.Mappers;
using MetaCortex.Payments.DataAccess.Entities;
using MetaCortex.Payments.DataAccess.Interfaces;

namespace MetaCortex.Payments.API.RabbitMq;

public class ProcessConsumedOrderService
{
    private readonly IProcessedOrderRepository _processedOrderRepository;
    private readonly MapOrderToIsPaidIsTrue _mapper;

    public ProcessConsumedOrderService(IProcessedOrderRepository processedOrderRepository)
    {
        _processedOrderRepository = processedOrderRepository;
        _mapper = new MapOrderToIsPaidIsTrue();
    }

    public Task<ProcessedOrder?> ProcessOrderAsync(string order)
    {
        var deserializedOrder = JsonSerializer.Deserialize<ProcessedOrder>(order);

        switch (deserializedOrder?.PaymentMethod)
        {
            case "CreditCard":
                return Task.FromResult(_mapper.MapIncomingCreditCardOrder(order));
                
            case "Swish":
                return Task.FromResult(_mapper.MapIncomingSwishOrder(order));

            case "Klarna":
                return Task.FromResult(_mapper.MapIncomingKlarnaOrder(order));

            case "Stripe":
                return Task.FromResult(_mapper.MapIncomingStripeOrder(order));

            default:
                return Task.FromResult(_mapper.MapIncomingInvalidPayment(order));
        }
    }
}