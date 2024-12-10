namespace MetaCortex.Payments.DataAccess.RabbitMq;

public interface IMessageProducerService
{
    Task SendPaymentToOrderAsync<T>(T order, string sendChannel);
}