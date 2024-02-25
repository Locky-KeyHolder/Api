using Locky.Keyholder.Core;
using Locky.Keyholder.Core.Services;
using Marten;

namespace Locky.Keyholder.Infrastructure.Data;

public class MatrenAggregateRepository<T>: IAggregateRepository<T> where T: AggregateBase
{
    
    private readonly IDocumentStore _documentStore;
    
    public async Task<T> LoadAsync(Guid id, long version, CancellationToken cancellationToken = default)
    {
        await using var session = await _documentStore.LightweightSerializableSessionAsync(cancellationToken);
        var aggregate = await session.Events.AggregateStreamAsync<T>(id,version ,token: cancellationToken);
        if (aggregate == null)
        {
            throw new AggregateNotFoundException(id, typeof(T));
        }
        return aggregate;
    }

    public async Task StoreAsync(T aggregate, CancellationToken cancellationToken = default)
    {
        await using var session = await _documentStore.LightweightSerializableSessionAsync(cancellationToken);
        var events = aggregate.GetUncommittedEvents().ToArray();
        session.Events.Append(aggregate.Id, aggregate.Version+events.Length, events,  cancellationToken);
        await session.SaveChangesAsync(cancellationToken);
        aggregate.ClearUncommittedEvents();
    }
}