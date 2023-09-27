using System.ComponentModel.DataAnnotations;

namespace Client_Side.Models.Products
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public double Price { get; set; }
        [Required]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
    }
}
