namespace Locky.Keyholder.Infrastructure.Data;

public sealed class AggregateNotFoundException : Exception
{
    public AggregateNotFoundException(Guid id, Type type) : base($"Aggregate of type {type.Name} with id {id} was not found.")
    {
    }
}