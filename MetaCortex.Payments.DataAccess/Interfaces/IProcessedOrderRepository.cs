using MetaCortex.Payments.DataAccess.Entities;

namespace MetaCortex.Payments.DataAccess.Interfaces;

public interface IProcessedOrderRepository : IRepository<ProcessedOrder, string>
{
    
}