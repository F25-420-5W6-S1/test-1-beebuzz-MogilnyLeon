using BeeBuzz.Data.Entities;
using BeeBuzz.Data.Interfaces;

namespace BeeBuzz.Data.Repositories
{
    public class BeeBuzzApplicationUserRepository : BeeBuzzGenericGenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        public BeeBuzzApplicationUserRepository(ApplicationDbContext db, ILogger<BeeBuzzGenericGenericRepository<ApplicationUser>> logger) : base(db, logger)
        {
        }
    }
}
