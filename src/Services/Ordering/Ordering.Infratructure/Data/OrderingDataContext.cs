using Ordering.Application.Data;
using System.Reflection;

namespace Ordering.Infrastructure.Data
{
    public sealed class OrderingDataContext : DbContext, IOrderingDataContext
    {
        public OrderingDataContext(DbContextOptions<OrderingDataContext> opts) : base(opts)
        {
            
        }
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
