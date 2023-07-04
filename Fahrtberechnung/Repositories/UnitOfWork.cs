using Fahrtberechnung.DbContexts;
using Fahrtberechnung.IRepostories;
using Fahrtberechnung.Models;

namespace Fahrtberechnung.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly DatabaseDbContext dbContext;
        public IGenericRepository<User> Users { get; }

        public UnitOfWork(DatabaseDbContext databaseDbContext)
        {
            this.dbContext = databaseDbContext;
            Users = new GenericRepository<User>(dbContext);
        }

        public async ValueTask SaveChangesAsync() =>
              await dbContext.SaveChangesAsync();
    }
}
