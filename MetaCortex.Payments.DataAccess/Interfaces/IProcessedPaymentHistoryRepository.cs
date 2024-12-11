using MetaCortex.Payments.DataAccess.Entities;

namespace MetaCortex.Payments.DataAccess.Interfaces;

public interface IProcessedPaymentHistoryRepository : IRepository<PaymentHistory, string>
{
    
}