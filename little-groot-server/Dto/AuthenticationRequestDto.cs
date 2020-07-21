using System.ComponentModel.DataAnnotations;

namespace LittleGrootServer.Dto {
    public class AuthenticationRequestDto {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}