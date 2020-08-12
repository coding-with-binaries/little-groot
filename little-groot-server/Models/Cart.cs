using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LittleGrootServer.Models {
    public class Cart {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Cart Id")]
        public long Id { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public List<CartItem> CartItems { get; set; }
    }
}
