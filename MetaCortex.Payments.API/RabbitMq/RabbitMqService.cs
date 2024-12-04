using System.Runtime.InteropServices.ComTypes;
using MetaCortex.Payments.DataAccess.RabbitMq;
using RabbitMQ.Client;

namespace MetaCortex.Payments.API.RabbitMq;

public class RabbitMqService : IRabbitMqService
{
    private readonly RabbitMqConfiguration config;

    public RabbitMqService(RabbitMqConfiguration config)
    {
        this.config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public Task<IConnection> CreateConnection()
    {
        if (config == null)
        {
            throw new InvalidOperationException("Configuration is not initialized.");
        }

        var connectionFactory = new ConnectionFactory
        {
            HostName = config.HostName,
            UserName = config.Username,
            Password = config.Password,
            VirtualHost = "/",
        };
        var connection = connectionFactory.CreateConnectionAsync();
        return connection;
    }
}