using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions
{
    public sealed class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(Guid Id) : base($"Order", Id)
        {
        }
    }
}
