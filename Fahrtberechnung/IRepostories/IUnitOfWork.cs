using Fahrtberechnung.Models;

namespace Fahrtberechnung.IRepostories
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get; }
        ValueTask SaveChangesAsync();

    }
}
