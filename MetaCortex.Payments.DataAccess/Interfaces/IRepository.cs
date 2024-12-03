namespace MetaCortex.Payments.DataAccess.Interfaces;

public interface IRepository<IEntity, Tid> where IEntity : IEntity<Tid>
{
    Task<IEnumerable<IEntity>> GetAllAsync();
    Task<IEntity> GetByIdAsync(Tid id);
    Task AddAsync(IEntity entity);
    Task UpdateAsync(IEntity entity);
    Task DeleteAsync(Tid id);
}