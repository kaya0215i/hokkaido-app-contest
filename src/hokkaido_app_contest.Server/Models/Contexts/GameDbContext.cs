using Microsoft.EntityFrameworkCore;

namespace hokkaido_app_contest.Server.Models.Contexts {
    public class GameDbContext : DbContext {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }
    }
}
