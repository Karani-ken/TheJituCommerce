using TheJituEcommerce_OrderService.Models.Dtos;
using TheJituEcommerce_OrderService.Services.IService;
using TheJituEcommerce_OrderService.Data;
using AutoMapper;
using TheJituEcommerce_OrderService.Models;

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
            OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto);
            orderHeaderDto.Status = "Pending";
            orderHeaderDto.OrderDetails = _mapper.Map<IEnumerable<OrderDetailsDto>>(cartDto.CartDetails);
            orderHeaderDto.OrderTotal = Math.Round(orderHeaderDto.OrderTotal, 2);
            OrderHeader newOrder = _mapper.Map<OrderHeader>(orderHeaderDto);
            _appDbContext.OrderHeaders.Add(newOrder);
            await _appDbContext.SaveChangesAsync();
            orderHeaderDto.OrderHeaderId = orderHeaderDto.OrderHeaderId;
            return orderHeaderDto;
        }
    }
}
