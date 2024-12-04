using System.Runtime.InteropServices.ComTypes;
using RabbitMQ.Client;

namespace MetaCortex.Payments.DataAccess.RabbitMq;

public interface IRabbitMqService
{
   Task <IConnection> CreateConnection();
}