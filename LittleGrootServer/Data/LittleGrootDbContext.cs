using Microsoft.EntityFrameworkCore;
using LittleGrootServer.Models;

namespace LittleGrootServer.Data {
    public class LittleGrootDbContext : DbContext {

        public LittleGrootDbContext(DbContextOptions<LittleGrootDbContext> options) : base(options) { }

        public DbSet<Plant> Plants { get; set; }
    }
}
