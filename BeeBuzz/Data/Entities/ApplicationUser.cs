using Microsoft.AspNetCore.Identity;

namespace BeeBuzz.Data.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ICollection<Beehive> Beehives { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

    }
}
