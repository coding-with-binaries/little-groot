using System.ComponentModel.DataAnnotations;

namespace LittleGrootServer.Dto {
    public class AuthenticationDto {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}