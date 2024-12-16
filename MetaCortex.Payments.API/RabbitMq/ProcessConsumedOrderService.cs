using System.Text.Json;
using MetaCortex.Payments.API.Mappers;
using MetaCortex.Payments.DataAccess.Entities;
using MetaCortex.Payments.DataAccess.Interfaces;

namespace MetaCortex.Payments.API.RabbitMq;

public class ProcessConsumedOrderService
{
    private readonly MapOrderToIsPaidIsTrue _mapper = new();

    public Task<ProcessedOrder?> ProcessOrderAsync(string order)
    {
        // Metoderna inom min switch skiljer sig inte direkt jättemycket. Detta är mer en showcase av min ide om struktur för att hantera olika betalplaner. 
        // Jag har ju uppenbarligen inte implementerat logiken för att hantera de olika betalsystemen. Därför blir det lite "på låtsas".
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