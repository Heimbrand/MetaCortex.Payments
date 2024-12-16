using MetaCortex.Payments.DataAccess.Entities;

namespace MetaCortex.Payments.DataAccess.RabbitMq;

public interface IProcessConsumedOrderService
{
    Task<ProcessedOrder?> ProcessOrderAsync(string order);
}