using Coupons_Service.Data;

namespace Coupons_Service.Services.IServices
{
    public interface ICouponInterface
    {
        Task<IEnumerable<Coupon>> GetCouponsAsync();

        Task<Coupon> GetCouponByIdAsync(Guid id);

        Task<Coupon> GetCouponByNameAsync(string couponCode);

        Task<string> AddCouponAsync(Coupon coupon);
        Task<string> UpdateCouponAsync(Coupon coupon);
        Task<string> DeleteCouponAsync(Coupon coupon);
    }
}
