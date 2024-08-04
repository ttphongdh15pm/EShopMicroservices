namespace Ordering.Domain.Enums
{
    public enum OrderStatus : byte
    {
        Draft = 1,
        Pending = 2,
        Completed = 3,
        Cancelled = 4
    }
}