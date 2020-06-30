using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LittleGrootServer.Models {
    public class Plant {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Plant Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Plant Family")]
        public string Family { get; set; }

        [Required]
        [Display(Name = "Plant Type")]
        public string Type { get; set; }

        public string Description { get; set; }
    }
}
