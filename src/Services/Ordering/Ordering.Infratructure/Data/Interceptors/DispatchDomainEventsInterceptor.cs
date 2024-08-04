using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context);
            return base.SavingChanges(eventData, result);
        }
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => DispatchDomainEvents(eventData.Context));
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void DispatchDomainEvents(DbContext? context)
        {
            if(context is null)
            {
                return;
            }

            var aggregates = context.ChangeTracker
                .Entries<IAggregate>()
                .Where(agg => agg.Entity.DomainEvents.Any())
                .Select(agg => agg.Entity);


            var domainEvents = aggregates.SelectMany(agg => agg.DomainEvents);
            Parallel.ForEach(aggregates, aggregate =>
            {
                aggregate.ClearDomainEvents();
            });
            Parallel.ForEach(domainEvents, async domainEvent => {
                await mediator.Publish(domainEvent);
            });
        }
    }
}
