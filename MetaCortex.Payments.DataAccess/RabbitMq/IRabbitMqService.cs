using RabbitMQ.Client;

namespace MetaCortex.Payments.DataAccess.RabbitMq;

public interface IRabbitMqService
{
   Task <IConnection> CreateConnection();
}