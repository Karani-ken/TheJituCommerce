using Microsoft.EntityFrameworkCore;
using TheJitu_Ecommerce_Cart.Models;

namespace TheJitu_Ecommerce_Cart.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<CartDetails> CartDetails { get; set; }

        public DbSet<CartHeader> CartHeaders { get; set; }
    }
}
