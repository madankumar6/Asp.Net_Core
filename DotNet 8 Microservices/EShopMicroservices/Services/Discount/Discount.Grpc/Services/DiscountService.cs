using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext discountContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountContext.Coupons.Where(x => x.ProductName == request.ProductName).FirstOrDefaultAsync();
            if (coupon == null)
            {
                coupon = new Coupon() { Id = 0, ProductName = "No Discount", Amount = 0, Description = "No Discount" };
            }

            logger.LogInformation($"Discount is retrieved for {coupon.ProductName}, Amount {coupon.Amount}");
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
            }

            discountContext.Coupons.Add(coupon);
            await discountContext.SaveChangesAsync();

            logger.LogInformation($"Discount is successfully created. Product Name {coupon.ProductName}, Amount {coupon.Amount}");

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var couponRequest = request.Coupon;
            var coupon = await discountContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.Coupon.ProductName);
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));
            }

            coupon.Description = couponRequest.Description;
            coupon.Amount = couponRequest.Amount;
            discountContext.Coupons.Update(coupon);
            await discountContext.SaveChangesAsync();

            logger.LogInformation($"Discount is successfully updated. Product Name {coupon.ProductName}, Amount {coupon.Amount}");

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));
            }

            discountContext.Coupons.Remove(coupon);
            await discountContext.SaveChangesAsync();

            logger.LogInformation($"Discount is successfully deleted. Product Name {coupon.ProductName}");

            return new DeleteDiscountResponse() { Success = true };
        }
    }
}
