using BeeBuzz.Data.Entities;
using BeeBuzz.Data.Interfaces;

namespace BeeBuzz.Data.Repositories
{
    public class BeeBuzzBeehiveRepository : BeeBuzzGenericGenericRepository<Beehive>, IBeehiveRepository
    {
        public BeeBuzzBeehiveRepository(ApplicationDbContext db, ILogger<BeeBuzzGenericGenericRepository<Beehive>> logger) : base(db, logger)
        {
            
        }
        public IEnumerable<Beehive> GetByOrganization(int organizationId)
        {
            // select * from Beehive as B join AspNetUsers as U on B.UserId = U.Id where U.OrganizationId = 1
            return _dbSet.Where(b => b.User.OrganizationId == organizationId). ToList();
        }
    }
}
