using System.Text.Json;
using MetaCortex.Payments.DataAccess.Entities;

namespace MetaCortex.Payments.API.Mappers;

public class MapOrderToIsPaidIsTrue
{
    public ProcessedOrder? MapIncomingCreditCardOrder(string order)
    {
        var orderDto = JsonSerializer.Deserialize<ProcessedOrder>(order);

        if (orderDto is not null)
        {
            var processOrder = new ProcessedOrder
            {
                Id = orderDto.Id,
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId,
                VIPStatus = orderDto.VIPStatus,
                PaymentPlan = new Payment
                {
                    PaymentMethod = orderDto.PaymentMethod,
                    IsPaid = true,
                }
            };

            if (orderDto.Products is not null)
            {
                orderDto.Products.ForEach(product =>
                {
                    new Products
                    {
                        id = product.id,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = product.Quantity
                    };
                });
            }
            return processOrder;
        }
        return null;
    }
    public ProcessedOrder? MapIncomingSwishOrder(string order)
    {
        var orderDto = JsonSerializer.Deserialize<ProcessedOrder>(order);

        if (orderDto is not null)
        {
            var processOrder = new ProcessedOrder
            {
                Id = orderDto.Id,
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId,
                VIPStatus = orderDto.VIPStatus,
                PaymentPlan = new Payment
                {
                    PaymentMethod = orderDto.PaymentMethod,
                    IsPaid = true,
                }
            };

            if (orderDto.Products is not null)
            {
                orderDto.Products.ForEach(product =>
                {
                    new Products
                    {
                        id = product.id,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = product.Quantity
                    };
                });
            }
            return processOrder;
        }
        return null;
    }
    public ProcessedOrder? MapIncomingKlarnaOrder(string order)
    {
        var orderDto = JsonSerializer.Deserialize<ProcessedOrder>(order);

        if (orderDto is not null)
        {
            var processOrder = new ProcessedOrder
            {
                Id = orderDto.Id,
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId,
                VIPStatus = orderDto.VIPStatus,
                PaymentPlan = new Payment
                {
                    PaymentMethod = orderDto.PaymentMethod,
                    IsPaid = true,
                }
            };

            if (orderDto.Products is not null)
            {
                orderDto.Products.ForEach(product =>
                {
                    new Products
                    {
                        id = product.id,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = product.Quantity
                    };
                });
            }
            return processOrder;
        }
        return null;
    }
    public ProcessedOrder? MapIncomingStripeOrder(string order)
    {
        var orderDto = JsonSerializer.Deserialize<ProcessedOrder>(order);

        if (orderDto is not null)
        {
            var processOrder = new ProcessedOrder
            {
                Id = orderDto.Id,
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId,
                VIPStatus = orderDto.VIPStatus,
                PaymentPlan = new Payment
                {
                    PaymentMethod = orderDto.PaymentMethod,
                    IsPaid = true,
                }
            };

            if (orderDto.Products is not null)
            {
                orderDto.Products.ForEach(product =>
                {
                    new Products
                    {
                        id = product.id,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = product.Quantity
                    };
                });
            }
            return processOrder;
        }
        return null;
    }
    public ProcessedOrder? MapIncomingInvalidPayment(string order)
    {
        var orderDto = JsonSerializer.Deserialize<ProcessedOrder>(order);

        if (orderDto is not null)
        {
            var processOrder = new ProcessedOrder
            {
                Id = orderDto.Id,
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId,
                VIPStatus = orderDto.VIPStatus,
                PaymentPlan = new Payment
                {
                    PaymentMethod = "Invalid payment method",
                    IsPaid = false,
                }
            };

            if (orderDto.Products is not null)
            {
                orderDto.Products.ForEach(product =>
                {
                    new Products
                    {
                        id = product.id,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = product.Quantity
                    };
                });
            }
            return processOrder;
        }
        return null;
    }
}