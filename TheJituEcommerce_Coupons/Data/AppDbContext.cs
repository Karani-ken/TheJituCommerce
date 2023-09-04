using Microsoft.EntityFrameworkCore;
using TheJituEcommerce_Coupons.Models;

namespace TheJituEcommerce_Coupons.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) { }



        public DbSet<Coupon> Coupons { get; set; }
    }
}

