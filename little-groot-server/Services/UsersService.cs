using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LittleGrootServer.Data;
using LittleGrootServer.Models;

namespace LittleGrootServer.Services {
    public interface IUsersService {
        Task<IEnumerable<User>> GetUsers();
    }

    public class UsersService : IUsersService {

        private LittleGrootDbContext _dbContext = null;

        public UsersService(LittleGrootDbContext context) {
            this._dbContext = context;
        }

        public async Task<IEnumerable<User>> GetUsers() {
            return await _dbContext.Users.ToListAsync();
        }
    }
}