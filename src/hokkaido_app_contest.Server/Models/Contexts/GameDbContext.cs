using hokkaido_app_contest.Shared.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace hokkaido_app_contest.Server.Models.Contexts {
    public class GameDbContext : DbContext {
        public DbSet<User> Users { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }
    }
}
