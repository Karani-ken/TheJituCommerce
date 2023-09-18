using TheJituEcommerce_OrderService.Models.Dtos;
using TheJituEcommerce_OrderService.Services.IService;
using TheJituEcommerce_OrderService.Data;
using AutoMapper;
using TheJituEcommerce_OrderService.Models;
using Stripe.Checkout;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace TheJituEcommerce_OrderService.Services
{
    public class OrderService : IOrderInterface
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public OrderService(AppDbContext context, IMapper mapper)
        {
            _appDbContext = context;
            _mapper = mapper;                
        }
        public async Task<OrderHeaderDto> CreateOrderHeader(CartDto cartDto)
        {
            OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto.CartHeader);
            orderHeaderDto.Status = "Pending";
            orderHeaderDto.OrderDetails = _mapper.Map<IEnumerable<OrderDetailsDto>>(cartDto.CartDetails);
            orderHeaderDto.OrderTotal = Math.Round(orderHeaderDto.OrderTotal, 2);
            OrderHeader newOrder = _mapper.Map<OrderHeader>(orderHeaderDto);
           var  Item = _appDbContext.OrderHeaders.Add(newOrder).Entity;
            await _appDbContext.SaveChangesAsync();
            orderHeaderDto.OrderHeaderId = Item.OrderHeaderId;
            return orderHeaderDto;
        }

        public async Task<StripeRequestDto> StripePayment(StripeRequestDto stripeRequestDto)
        {
            var options = new SessionCreateOptions()
            {
                SuccessUrl = stripeRequestDto.ApprovedUrl, // where the customer will be redirected after a successfull payment
                CancelUrl = stripeRequestDto.CancelUrl, //where the customer will be redirected in case of failure
                Mode = "payment", //setting the stripe mode into payments modes can be payments or subscriptions
                LineItems = new List<SessionLineItemOptions>() // a list of products including  their details such as price, name etc
            };
            //populating the lineList Items
            foreach(var item in stripeRequestDto.OrderHeader.OrderDetails)
            {
                // Create a new SessionLineItemOptions for each order detail.
                var sessionLineItems = new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions() //represents the pricing information for the line items
                    {
                        //set the unit amount (price) for the line items
                        UnitAmount = (long)(item.Price * 100), //converts from decimal to cents eg from $10.50 to 1050 cents
                        Currency = "kes", //set the currency (kenyan shilling for this case
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = item.ProductName //set the name of the product
                        },
                    },
                    Quantity = item.Count //set the quantity of the product
                };
                options.LineItems.Add(sessionLineItems);
            }
            var DiscountObj = new List<SessionDiscountOptions>() //list to store the discount options that can be applied
            {
                new SessionDiscountOptions()
                {
                    Coupon=stripeRequestDto.OrderHeader.CouponCode //where the coupon code is stored
                }
            };
            if (stripeRequestDto.OrderHeader.Discount > 0)
            {
                options.Discounts = DiscountObj;
            }
            var service = new SessionService();
            Session session = service.Create(options);

            stripeRequestDto.StripeSessionId = session.Id;

            stripeRequestDto.StripeSessionUrl = session.Url;

            OrderHeader order = await _appDbContext.OrderHeaders.FirstOrDefaultAsync(x => x.OrderHeaderId == stripeRequestDto.OrderHeader.OrderHeaderId);

            order.StripeSessionId = session.Id;
            await _appDbContext.SaveChangesAsync();

            return stripeRequestDto;
        }

        public async Task<bool> ValidatePayment(Guid OrderId)
        {
            OrderHeader order = await _appDbContext.OrderHeaders.FirstOrDefaultAsync(x => x.OrderHeaderId == OrderId);
            var service = new SessionService();
            Session session = service.Get(order.StripeSessionId);

            var paymentIntentService = new PaymentIntentService();
            var id = session.PaymentIntentId;
            if(id == null)
            {
                return false;
            }
            PaymentIntent paymentInt = paymentIntentService.Get(id);
            if(paymentInt.Status == "succeeded")
            {
                order.PaymentIntentId = paymentInt.Id;
                order.Status = "Approved";
                await _appDbContext.SaveChangesAsync();
                return true;

                //communicate with rewards topic
            }
            return false;

        }
    }
}
