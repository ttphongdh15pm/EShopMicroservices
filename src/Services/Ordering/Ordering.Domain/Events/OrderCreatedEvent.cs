﻿namespace Ordering.Domain.Events
{
    public record OrderCreatedEvent(Order order) : IEventDomain;
}
