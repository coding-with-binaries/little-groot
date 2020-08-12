using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LittleGrootServer.Models {
    public class CartItem {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Cart Item Id")]
        public long Id { get; set; }

        public Plant Plant { get; set; }
        public int Quantity { get; set; }

        public long CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
