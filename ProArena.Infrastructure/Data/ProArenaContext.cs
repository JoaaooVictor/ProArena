using Microsoft.EntityFrameworkCore;

namespace ProArena.Infrastructure.Data
{
    public class ProArenaContext : DbContext
    {
        public ProArenaContext(DbContextOptions options) : base(options) { }
    }
}
