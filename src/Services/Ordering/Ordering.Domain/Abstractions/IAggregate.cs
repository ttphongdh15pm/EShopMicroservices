﻿namespace Ordering.Domain.Abstractions
{
    public interface IAggregate<T> : IAggregate, IEntity<T>
    {

    }
    public interface IAggregate : IEntity
    {
        IReadOnlyList<IEventDomain> DomainEvents { get; }
        IEventDomain[] ClearDomainEvents();
    }
}
