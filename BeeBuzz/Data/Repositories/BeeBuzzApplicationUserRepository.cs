using BeeBuzz.Data.Entities;
using BeeBuzz.Data.Interfaces;

namespace BeeBuzz.Data.Repositories
{
    public class BeeBuzzApplicationUserRepository : BeeBuzzGenericGenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        public BeeBuzzApplicationUserRepository(ApplicationDbContext db, ILogger<BeeBuzzGenericGenericRepository<ApplicationUser>> logger) : base(db, logger)
        {
            
        }

        public IEnumerable<ApplicationUser> GetByOrganization(int organizationId)
        {
            return _dbSet.Where(u => u.OrganizationId == organizationId).ToList();
        }
    }
}
