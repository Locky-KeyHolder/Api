namespace Locky.Keyholder.Core.Services;

public interface IAggregateRepository<T> where T: AggregateBase
{
    public Task<T> LoadAsync(Guid id,long version , CancellationToken cancellationToken = default);
    public Task StoreAsync(T aggregate, CancellationToken cancellationToken = default);
}