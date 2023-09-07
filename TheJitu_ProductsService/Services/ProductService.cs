using Microsoft.EntityFrameworkCore;
using TheJitu_ProductsService.Data;
using TheJitu_ProductsService.Models;
using TheJitu_ProductsService.Models.Dtos;

namespace TheJitu_ProductsService.Services
{
    public class ProductService : IProductInterface
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;  
        }
        public async Task<string> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return "Product added successfully";
           
        }

        public async Task<string> DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return "Product Deleted successfully";
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
          
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<string> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return "Product updated successfully";
        }
    }
}
