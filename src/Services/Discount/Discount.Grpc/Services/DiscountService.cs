using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountDataContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName, context.CancellationToken);

            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            }
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully deleted. ProductName : {productName}", request.ProductName);
            return new DeleteDiscountResponse
            {
                Success = true
            };
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var countpon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName, context.CancellationToken);
            if (countpon is null)
            {
                countpon = new Coupon
                {
                    ProductName = request.ProductName,
                    Amount = 0,
                    Description = "No Discount"
                };
            }
            logger.LogInformation("Discount is retrieved for Product: {productName}", request.ProductName);
            return countpon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            }
            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discouynt is successfully updated. Product name: {productName}", coupon.ProductName);
            return coupon.Adapt<CouponModel>();
        }
    }
}
