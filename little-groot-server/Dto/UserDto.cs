using System.ComponentModel.DataAnnotations;

namespace LittleGrootServer.Dto {
    public class UserDto {

        public long Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}