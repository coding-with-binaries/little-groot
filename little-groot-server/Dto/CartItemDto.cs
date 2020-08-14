namespace LittleGrootServer.Dto {
    public class CartItemDto {
        public long Id { get; set; }
        public PlantDto Plant { get; set; }
        public int Quantity { get; set; }
    }
}