using System.Runtime.InteropServices.ComTypes;
using MetaCortex.Payments.DataAccess.RabbitMq;
using RabbitMQ.Client;

namespace MetaCortex.Payments.API.RabbitMq;

public class RabbitMqService(RabbitMqConfiguration config) : IRabbitMqService
{
    private readonly RabbitMqConfiguration config;
    public Task<IConnection>  CreateConnection()
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = config.HostName,
            UserName = config.Username,
            Password = config.Password,
            VirtualHost = "/",

        };
        var connection =  connectionFactory.CreateConnectionAsync();
        return connection;
    }
}