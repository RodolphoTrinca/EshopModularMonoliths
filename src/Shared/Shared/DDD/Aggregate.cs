namespace Shared.DDD;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent == null) 
            throw new ArgumentNullException($"The {nameof(domainEvent)} cannot be null");

        _domainEvents.Add(domainEvent);
    }
    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent == null)
            throw new ArgumentNullException($"The {nameof(domainEvent)} cannot be null");

        _domainEvents.Remove(domainEvent);
    }

    public IDomainEvent[] ClearDomainEvents()
    {
        var events = _domainEvents.ToArray();
        _domainEvents.Clear();
        return events;
    }
}
