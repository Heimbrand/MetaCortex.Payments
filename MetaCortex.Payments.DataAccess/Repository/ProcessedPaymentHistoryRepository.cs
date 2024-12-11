using MetaCortex.Payments.DataAccess.Entities;
using MetaCortex.Payments.DataAccess.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Options;


namespace MetaCortex.Payments.DataAccess.Repository;

public class ProcessedPaymentHistoryRepository : IProcessedPaymentHistoryRepository
{
    private readonly IMongoCollection<PaymentHistory> _collection;

    public ProcessedPaymentHistoryRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> mongoDbSettings)
    {
        var setting = mongoDbSettings.Value;
        var database = mongoClient.GetDatabase(setting.DatabaseName);
        _collection = database.GetCollection<PaymentHistory>(setting.CollectionName, new MongoCollectionSettings { AssignIdOnInsert = true });
    }
    public async Task<IEnumerable<PaymentHistory>> GetAllAsync()
    {
        var payments = await _collection.Find(new BsonDocument()).ToListAsync();

        if (payments is null || !payments.Any())
            throw new Exception("No payments found");
        
        return payments;
    }
    public async Task<PaymentHistory> GetByOrderIdAsync(string orderId)
    {
        var paymentByid =  await _collection.Find(x => x.Id == orderId).FirstOrDefaultAsync();

        if (paymentByid is null)
            throw new Exception("Payment not found");

        return paymentByid;
    }
    public async Task AddAsync(PaymentHistory entity)
    {
        await _collection.InsertOneAsync(entity);
    }
    public async Task UpdateAsync(PaymentHistory entity)
    {
        await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }
    public async Task DeleteAsync(string id)
    {
        var PaymentToDelete = await GetByOrderIdAsync(id);

        await _collection.DeleteOneAsync(x => x.Id == PaymentToDelete.Id);
    }
}