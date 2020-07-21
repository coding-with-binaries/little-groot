using System.ComponentModel.DataAnnotations;

namespace LittleGrootServer.Dto {
    public class AuthenticationResponseDto {

        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }
    }
}