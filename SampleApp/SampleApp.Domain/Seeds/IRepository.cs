namespace SampleApp.Domain.Seeds;

public interface IRepository<TKey, TValue>
    where TKey : struct
    where TValue : IAggregateRoot
{
    Task<TValue?> GetByIdAsync(TKey id);
    Task UpdateAsync(TKey id, TValue item);
    Task CreateAsync(TValue item);
    Task DeleteAsync(TKey id);
}
