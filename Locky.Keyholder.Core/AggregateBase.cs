using System.Text.Json.Serialization;

namespace Locky.Keyholder.Core;

public class AggregateBase
{
    public Guid Id { get;  set; }
    public long Version { get;  set; }
    
    // JsonIgnore - for making sure that it won't be stored in inline projection
    [JsonIgnore] private readonly List<object> _uncommittedEvents = new List<object>();

    // Get the deltas, i.e. events that make up the state, not yet persisted
    public IEnumerable<object> GetUncommittedEvents()
    {
        return _uncommittedEvents;
    }

    // Mark the deltas as persisted.
    public void ClearUncommittedEvents()
    {
        _uncommittedEvents.Clear();
    }

    protected void AddUncommittedEvent(object @event)
    {
        // add the event to the uncommitted list
        _uncommittedEvents.Add(@event);
    }
}