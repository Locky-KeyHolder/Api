namespace Locky.Keyholder.Core.Services;

public interface IReadmodelLoader<T>
{
    public Task<T> LoadAsync(Guid id, CancellationToken cancellationToken = default);
}