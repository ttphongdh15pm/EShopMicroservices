namespace Ordering.Domain.Abstractions
{
    public interface IEntity<T> : IEntity
    {
        T Id { get; set; }
    }
    public interface IEntity
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
        DateTime? LastModifiedAt { get; set; }
        string LastModifiedBy { get; set; }
    }
}
