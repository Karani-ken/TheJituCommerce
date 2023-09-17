using Microsoft.EntityFrameworkCore;
using TheJituEcommerce_OrderService.Models;

namespace TheJituEcommerce_OrderService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
    }
}
