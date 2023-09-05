using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TheJitu_Ecommerce_Cart.Models.Dtos;

namespace TheJitu_Ecommerce_Cart.Models
{
    public class CartDetails
    {
        [Key]
        public Guid CartDetailsId { get; set; }


        public Guid CartHeaderId { get; set; }

        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }


        public Guid ProductId { get; set; }

        [NotMapped]
        public ProductDto? Product { get; set; }

        public int Count { get; set; }
    }
}
