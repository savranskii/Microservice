namespace SampleApp.Domain.Seeds;

public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveEntitiesAsync(CancellationToken ct = default);
}
