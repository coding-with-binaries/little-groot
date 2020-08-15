using Microsoft.EntityFrameworkCore;
using LittleGrootServer.Models;
using System;

namespace LittleGrootServer.Data {
    public class LittleGrootDbContext : DbContext {

        public LittleGrootDbContext(DbContextOptions<LittleGrootDbContext> options) : base(options) { }

        public DbSet<Plant> Plants { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().HasAlternateKey(u => u.Email);
        }
    }
}
