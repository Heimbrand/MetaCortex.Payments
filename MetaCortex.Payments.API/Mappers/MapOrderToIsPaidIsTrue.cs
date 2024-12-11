using System.Text.Json;
using MetaCortex.Payments.DataAccess.Entities;

namespace MetaCortex.Payments.API.Mappers;

public class MapOrderToIsPaidIsTrue
{
    public ProcessedOrder MapIncomingCreditCardOrder(string order)
    {
        var orderDto = JsonSerializer.Deserialize<ProcessedOrder>(order);

        if (orderDto is not null)
        {
            return new ProcessedOrder
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
                    IsPaid =true,
                }
            };
        }

        return null;
    }
}