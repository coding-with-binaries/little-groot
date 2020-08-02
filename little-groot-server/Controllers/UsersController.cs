using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LittleGrootServer.Dto;
using LittleGrootServer.Services;
using LittleGrootServer.Exceptions;

namespace LittleGrootServer.Controllers {
    [Authorize]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class UsersController : ControllerBase {
        private IUsersService _usersService = null;

        public UsersController(IUsersService usersService) {
            _usersService = usersService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers() {
            var userDtos = _usersService.GetUsers();
            return Ok(userDtos);
        }

        [AllowAnonymous]
        [HttpGet("{email}/availability")]
        public IActionResult CheckEmailAvailability(string email) {
            var emailAvailable = _usersService.IsEmailAvailable(email);

            return Ok(new {
                emailAvailable = emailAvailable
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<UserDto> Register([FromBody] RegistrationDto registrationDto) {
            try {
                var userDto = _usersService.Register(registrationDto);
                return Ok(userDto);
            } catch (LittleGrootRegistrationException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<AuthenticationResponseDto> Authenticate([FromBody] AuthenticationRequestDto authenticationRequestDto) {
            var authenticationResponseDto = _usersService.Authenticate(authenticationRequestDto);
            if (authenticationResponseDto == null)
                return BadRequest("Username and password are incorrect");

            return Ok(authenticationResponseDto);
        }

        [HttpGet("current-user")]
        public ActionResult<UserDto> GetCurrentUser() {
            var userDto = _usersService.GetCurrentUser();
            return Ok(userDto);
        }
    }
}