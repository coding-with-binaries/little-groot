using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LittleGrootServer.Models {
    public class User {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "User Id")]
        public long Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "E-mail Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
