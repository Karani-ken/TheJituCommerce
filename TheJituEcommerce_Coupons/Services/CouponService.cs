using Microsoft.EntityFrameworkCore;
using TheJituEcommerce_Coupons.Data;
using TheJituEcommerce_Coupons.Models;
using TheJituEcommerce_Coupons.Services.IService;

namespace TheJituEcommerce_Coupons.Services
{
    public class CouponService : ICouponInterface
    {
        private readonly AppDbContext _context;
        public CouponService(AppDbContext context)
        {
            _context= context;
        }
        public async Task<string> AddCouponAsync(Coupon coupon)
        {
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();
            return "Coupon Added successfully";
        }

        public async Task<string> DeleteCouponAsync(Coupon coupon)
        {
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            return "Coupon deleted successfully";          
        }

        public async Task<Coupon> GetCouponByIdAsync(Guid id)
        {
            return await _context.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);           
        }

        public async Task<Coupon> GetCouponByNameAsync(string couponCode)
        {
            return await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode.ToLower() == couponCode.ToLower());
          
        }

        public async Task<IEnumerable<Coupon>> GetCouponsAsync()
        {
            return await _context.Coupons.ToListAsync();
           
        }

        public async Task<string> UpdateCouponAsync(Coupon coupon)
        {
            _context.Coupons.Update(coupon);
            await _context.SaveChangesAsync();
            return "Coupon Updated  successfully";
           
        }
    }
}
