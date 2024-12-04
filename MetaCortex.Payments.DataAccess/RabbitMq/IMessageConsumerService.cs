using MetaCortex.Payments.DataAccess.Entities;

namespace MetaCortex.Payments.DataAccess.RabbitMq;

public interface IMessageConsumerService
{
    Task ReadMessagesAsync();
    
}