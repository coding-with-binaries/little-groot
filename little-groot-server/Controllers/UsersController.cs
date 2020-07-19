using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LittleGrootServer.Services;
using LittleGrootServer.Models;

namespace LittleGrootServer.Controllers {
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private IUsersService _usersService = null;

        public UsersController(IUsersService usersService) {
            this._usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetPlants() {
            var plants = await _usersService.GetUsers();
            return Ok(plants);
        }
    }
}