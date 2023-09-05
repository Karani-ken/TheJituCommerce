using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheJitu_Ecommerce_Cart.Data;
using TheJitu_Ecommerce_Cart.Models;
using TheJitu_Ecommerce_Cart.Models.Dtos;
using TheJitu_Ecommerce_Cart.Services.IServices;

namespace TheJitu_Ecommerce_Cart.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CartService(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<bool> ApplyCoupons(CartDto cartDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CartUpsert(CartDto cartDto)
        {
            //check if this the first item that 
            //the user has added to the cart or if the cartheader exists
            CartHeader cartHeaderFromDb = await _context.CartHeaders.FirstOrDefaultAsync(h => 
            h.UserId == cartDto.CartHeader.UserId);
            if (cartHeaderFromDb == null)
            {
                //no cart header exists thus create
                //a new cart header in the db
                var newHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                  _context.CartHeaders.Add(newHeader);
                await _context.SaveChangesAsync();
                //add cartDetails  and assign the header above
                cartDto.CartDetails.First().CartHeaderId = newHeader.CartHeaderId;
                var cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
                _context.CartDetails.Add(cartDetails);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                //the cart header exists and user is adding a new Item or Updating the count of an existing item
                var  CartDetailsFromDb = await _context.CartDetails.FirstOrDefaultAsync(x=>x.ProductId ==
                cartDto.CartDetails.First().ProductId && x.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                if (CartDetailsFromDb == null)
                {
                    //Its a new product so we add to cart
                    cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                    var cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
                    _context.CartDetails.Add(cartDetails);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //we are updating the count of a product
                    CartDetailsFromDb.Count += cartDto.CartDetails.First().Count;
                    var cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
                    _context.CartDetails.Add(cartDetails);
                    await _context.SaveChangesAsync();

                }
                return true;
            }

            return false;
        }

        public Task<CartDto> GetUserCart(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromCart(Guid CartDetailId)
        {
            throw new NotImplementedException();
        }
    }
}
