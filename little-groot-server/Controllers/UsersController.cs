using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LittleGrootServer.Dto;
using LittleGrootServer.Services;
using LittleGrootServer.Exceptions;

namespace LittleGrootServer.Controllers {
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private IUsersService _usersService = null;

        public UsersController(IUsersService usersService) {
            this._usersService = usersService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers() {
            var userDtos = await _usersService.GetUsers();
            return Ok(userDtos);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegistrationDto registrationDto) {
            try {
                var userDto = await _usersService.Register(registrationDto);
                return Ok(userDto);
            } catch (LittleGrootRegistrationException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponseDto>> Authenticate([FromBody] AuthenticationRequestDto authenticationRequestDto) {
            var authenticationResponseDto = await _usersService.Authenticate(authenticationRequestDto);
            if (authenticationResponseDto == null)
                return BadRequest("Username and password are incorrect");

            return Ok(authenticationResponseDto);
        }
    }
}