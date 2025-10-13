using Microsoft.EntityFrameworkCore;

namespace SaaS.src.Infrastructure
{
    public class AppDbContext : DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
