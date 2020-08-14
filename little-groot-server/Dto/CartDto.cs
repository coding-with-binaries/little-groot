using System.Collections.Generic;

namespace LittleGrootServer.Dto {
    public class CartDto {
        public long Id { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}