using MediatR;

namespace Ordering.Domain.Abstractions
{
    public interface IEventDomain : INotification
    {
        Guid EventId => Guid.NewGuid();
        DateTime OccurredOn => DateTime.UtcNow;
        string EventType => GetType().AssemblyQualifiedName!;
    }
}
