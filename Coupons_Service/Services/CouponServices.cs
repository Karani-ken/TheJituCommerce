using Coupons_Service.Data;
using Coupons_Service.Services.IServices;

namespace Coupons_Service.Services
{
    public class CouponServices : ICouponInterface
    {
        
        public CouponServices()
        {
            
        }
        public Task<string> AddCouponAsync(Coupon coupon)
        {
            
            throw new NotImplementedException();
        }

        public Task<string> DeleteCouponAsync(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task<Coupon> GetCouponByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Coupon> GetCouponByNameAsync(string couponCode)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Coupon>> GetCouponsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateCouponAsync(Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
