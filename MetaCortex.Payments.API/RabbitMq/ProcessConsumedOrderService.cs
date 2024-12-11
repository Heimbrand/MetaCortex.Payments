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

    public async Task<ProcessedOrder> ProcessOrderAsync(string order)
    {
        var deserializedOrder = JsonSerializer.Deserialize<ProcessedOrder>(order);

        switch (deserializedOrder.PaymentMethod)
        {
            case "CreditCard":
                return _mapper.MapIncomingCreditCardOrder(order);
                
            case "Swish":
                return _mapper.MapIncomingSwishOrder(order);

            case "Klarna":
                return _mapper.MapIncomingKlarnaOrder(order);

            case "Stripe":
                return _mapper.MapIncomingStripeOrder(order);

            default:
                return _mapper.MapIncomingInvalidPayment(order);
        }
    }
}