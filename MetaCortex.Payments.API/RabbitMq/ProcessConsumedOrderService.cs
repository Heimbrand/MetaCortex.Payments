using System.Text.Json;
using MetaCortex.Payments.API.Mappers;
using MetaCortex.Payments.DataAccess.Entities;
using MetaCortex.Payments.DataAccess.Interfaces;

namespace MetaCortex.Payments.API.RabbitMq;

public class ProcessConsumedOrderService
{
    private readonly IProcessedOrderRepository _processedOrderRepository;
    private readonly MapOrderToIsPaidIsTrue _mapper;

    public ProcessConsumedOrderService(IProcessedOrderRepository processedOrderRepository)
    {
        _processedOrderRepository = processedOrderRepository;
        _mapper = new MapOrderToIsPaidIsTrue();
    }

    public async Task<ProcessedOrder> ProcessOrderAsync(string order)
    {
        var processedOrder = _mapper.MapIncomingOrderToIsPaid(order);

        if (processedOrder is not null)
        {
            await _processedOrderRepository.AddAsync(processedOrder);
            return processedOrder;
        }

        return null;
    }
}