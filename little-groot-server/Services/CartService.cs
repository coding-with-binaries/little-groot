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
            var cart = _dbContext.Cart.FirstOrDefault(c => c.UserId == long.Parse(currentUserId));

            return _mapper.Map<CartDto>(cart);
        }
    }
}