using BeeBuzz.Data.Entities;
using BeeBuzz.Data.Interfaces;

namespace BeeBuzz.Data.Repositories
{
    public class BeeBuzzOrganizationRepository : BeeBuzzGenericGenericRepository<Organization>, IOrganizationRepository
    {
        public BeeBuzzOrganizationRepository(ApplicationDbContext db, ILogger<BeeBuzzGenericGenericRepository<Organization>> logger) : base(db, logger)
        {
        }
    }
}
