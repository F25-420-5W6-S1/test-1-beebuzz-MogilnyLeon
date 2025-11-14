using System.ComponentModel.DataAnnotations;

namespace BeeBuzz.Data.Entities
{
    public class Organization
    {
        [Key]
        public int OrganisationId { get; set; }

        public string Name { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
