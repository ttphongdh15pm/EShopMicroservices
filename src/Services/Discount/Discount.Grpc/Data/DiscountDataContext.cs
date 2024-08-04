using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountDataContext : DbContext
    {
        public virtual DbSet<Coupon> Coupons { get; set; }
        public DiscountDataContext(DbContextOptions<DiscountDataContext> opts) : base(opts)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = 1,
                    ProductName = "T-shirt",
                    Description = "10% off on all T-shirts",
                    Amount = 10
                },
                new Coupon
                {
                    Id = 2,
                    ProductName = "Jeans",
                    Description = "Save $5 on any pair of jeans",
                    Amount = 5
                },
                new Coupon
                {
                    Id = 3,
                    ProductName = "Socks",
                    Description = "Buy 2 get 1 free on all socks",
                    Amount = 3
                }
            );
        }
    }
}
