using AutoMapper;
using LittleGrootServer.Data;
using LittleGrootServer.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace LittleGrootServer.Services {
    public interface ICartService {
        CartDto GetUserCart();
    }

    public class CartService : ICartService {

        private LittleGrootDbContext _dbContext = null;
        private readonly ILogger _logger;
        private IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CartService(LittleGrootDbContext context, ILogger<CartService> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) {
            _dbContext = context;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public CartDto GetUserCart() {
            var currentUserId = _httpContextAccessor.HttpContext.User.Identity.Name;
            var cart = _dbContext.Carts.FirstOrDefault(c => c.UserId == long.Parse(currentUserId));
            if (cart != null) {
                var cartItems = _dbContext.CartItems.Where(item => item.CartId == cart.Id).ToList();
                cartItems.ForEach(item => {
                    item.Plant = _dbContext.Plants.Find(item.PlantId);
                });
                cart.CartItems = cartItems;

                return _mapper.Map<CartDto>(cart);
            }
            return null;
        }
    }
}