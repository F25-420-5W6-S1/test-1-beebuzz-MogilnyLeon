using BeeBuzz.Data.Entities;
using BeeBuzz.Data.Interfaces;

namespace BeeBuzz.Data.Repositories
{
    public class BeeBuzzBeehiveRepository : BeeBuzzGenericGenericRepository<Beehive>, IBeehiveRepository
    {
        public BeeBuzzBeehiveRepository(ApplicationDbContext db, ILogger<BeeBuzzGenericGenericRepository<Beehive>> logger) : base(db, logger)
        {

        }
    }
}
