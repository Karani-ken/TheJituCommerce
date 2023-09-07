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
        private readonly IProductInterface _productInterface;
        private readonly ICouponService _couponService;
        public CartService(AppDbContext context,IMapper mapper,IProductInterface productInterface,ICouponService couponService)
        {
            _context = context;
            _mapper = mapper;
            _productInterface = productInterface;
            _couponService = couponService;
        }
        public async Task<bool> ApplyCoupons(CartDto cartDto) 
        {
            //get the header
            CartHeader cartHeaderFromDb = await _context.CartHeaders.FirstOrDefaultAsync(h => h.UserId ==
            cartDto.CartHeader.UserId);
            cartHeaderFromDb.CouponCode = cartDto.CartHeader.CouponCode;
            _context.CartHeaders.Update(cartHeaderFromDb);
            await _context.SaveChangesAsync();

            return true;
           
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
                CartDetails  CartDetailsFromDb = await _context.CartDetails.FirstOrDefaultAsync(x=>x.ProductId ==
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
                    _context.CartDetails.Update(CartDetailsFromDb);
                    await _context.SaveChangesAsync();     

                }
                return true;
            }

            return false;
        }

        public async Task<CartDto> GetUserCart(Guid userId)
        {
            var cartHeader =  _context.CartHeaders.FirstOrDefault(c => c.UserId == userId);
            var cartDetails =  _context.CartDetails.Where(x => x.CartHeaderId == cartHeader.CartHeaderId);
            CartDto cart = new CartDto()
            {
                CartHeader = _mapper.Map<CartHeaderDto>(cartHeader),
                CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(cartDetails)
            };
            //calculate Cart Total
            var products = await _productInterface.GetProductaAsync(); //get the products 
            foreach(var Item in cart.CartDetails)
            {
                Item.Product = products.FirstOrDefault(x => x.ProductId == Item.ProductId);
                cart.CartHeader.CartTotal += (int)(Item.Count * Item.Product.Price);
            }
            //Apply Coupon
            if (!string.IsNullOrWhiteSpace(cart.CartHeader.CouponCode))
            {
                //there is a coupon
                var Coupon = await _couponService.GetCouponData(cart.CartHeader.CouponCode);
                if(Coupon != null && cart.CartHeader.CartTotal > Coupon.CouponMinAmont)
                {
                    cart.CartHeader.CartTotal -= Coupon.CouponAmount;
                    cart.CartHeader.Discount = Coupon.CouponAmount;
                }
               
            }

            return cart;
            
        }

        public async Task<bool> RemoveFromCart(Guid CartDetailId)
        {    
            //get the cart to delete
            CartDetails cartDetails = await _context.CartDetails.FirstOrDefaultAsync(c=>c.CartDetailsId == CartDetailId);
            //check if the item is the lest item in the database
            var itemsCount = _context.CartDetails.Where(c => c.CartHeaderId == cartDetails.CartHeaderId).Count();

            _context.CartDetails.Remove(cartDetails);
            if(itemsCount == 1)
            {
                _context.CartHeaders.Remove(_context.CartHeaders.FirstOrDefault(x=>x.CartHeaderId==cartDetails.CartHeaderId));
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
