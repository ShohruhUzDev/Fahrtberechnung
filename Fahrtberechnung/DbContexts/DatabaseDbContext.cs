using Fahrtberechnung.Models;
using Microsoft.EntityFrameworkCore;

namespace Fahrtberechnung.DbContexts
{
    public class DatabaseDbContext :DbContext
    {
        public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options) : base(options) { 
        }
        public virtual DbSet<User> Users { get; set; }


    }
}
