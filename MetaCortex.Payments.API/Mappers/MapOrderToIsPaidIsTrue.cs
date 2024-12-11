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
                Id = orderDto.Id,
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId,
                VIPStatus = orderDto.VIPStatus,
                Products = orderDto.Products,
                PaymentPlan = new Payment
                {
                    PaymentMethod = orderDto.PaymentMethod,
                    IsPaid =true,
                }
            };
        }
        return null;
    }
    public ProcessedOrder MapIncomingSwishOrder(string order)
    {
        var orderDto = JsonSerializer.Deserialize<ProcessedOrder>(order);

        if (orderDto is not null)
        {
            return new ProcessedOrder
            {
                Id = orderDto.Id,
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId,
                VIPStatus = orderDto.VIPStatus,
                Products = orderDto.Products,
                PaymentPlan = new Payment
                {
                    PaymentMethod = orderDto.PaymentMethod,
                    IsPaid = true,
                }
            };
        }
        return null;
    }
    public ProcessedOrder MapIncomingKlarnaOrder(string order)
    {
        var orderDto = JsonSerializer.Deserialize<ProcessedOrder>(order);
        if (orderDto is not null)
        {
            return new ProcessedOrder
            {
                Id = orderDto.Id,
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId,
                VIPStatus = orderDto.VIPStatus,
                Products = orderDto.Products,
                PaymentPlan = new Payment
                {
                    PaymentMethod = orderDto.PaymentMethod,
                    IsPaid = true,
                }
            };
        }
        return null;
    }
    public ProcessedOrder MapIncomingStripeOrder(string order)
    {
        var orderDto = JsonSerializer.Deserialize<ProcessedOrder>(order);
        if (orderDto is not null)
        {
            return new ProcessedOrder
            {
                Id = orderDto.Id,
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId,
                VIPStatus = orderDto.VIPStatus,
                Products = orderDto.Products,
                PaymentPlan = new Payment
                {
                    PaymentMethod = orderDto.PaymentMethod,
                    IsPaid = true,
                }
            };
        }
        return null;
    }
    public ProcessedOrder MapIncomingInvalidPayment(string order)
    {
        var orderDto = JsonSerializer.Deserialize<ProcessedOrder>(order);

        if (orderDto is not null)
        {
            return new ProcessedOrder
            {
                Id = orderDto.Id,
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId,
                VIPStatus = orderDto.VIPStatus,
                Products = orderDto.Products,
                PaymentPlan = new Payment
                {
                    PaymentMethod = "Invalid payment",
                    IsPaid = false,
                }
            };
        }
        return null;
    }
}