namespace MetaCortex.Payments.DataAccess.RabbitMq;

public interface IMessageProducerService
{
    Task SendMessageAsync<T>(T order);
}