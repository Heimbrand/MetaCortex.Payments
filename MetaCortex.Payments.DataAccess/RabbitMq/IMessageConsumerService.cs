﻿namespace MetaCortex.Payments.DataAccess.RabbitMq;

public interface IMessageConsumerService
{
    Task ReadMessagesAsync(string queName);
    
}