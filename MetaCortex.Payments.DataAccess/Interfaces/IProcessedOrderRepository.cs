using MetaCortex.Payments.DataAccess.Entities;
using MongoDB.Bson;

namespace MetaCortex.Payments.DataAccess.Interfaces;

public interface IProcessedOrderRepository : IRepository<ProcessedOrder, string>
{
    
}