namespace Ordering.Domain.Abstractions
{
    public class Aggregate<TId> : Entity<TId>, IAggregate<TId>
    {
        private readonly List<IEventDomain> _domainEvents = new();
        public IReadOnlyList<IEventDomain> DomainEvents => _domainEvents.AsReadOnly();
        public void AddDomainEvent(IEventDomain eventDomain)
        {
            _domainEvents.Add(eventDomain);
        }
        public IEventDomain[] ClearDomainEvents()
        {
            IEventDomain[] domainEvents = _domainEvents.ToArray();
            _domainEvents.Clear();
            return domainEvents;
        }
    }
}
