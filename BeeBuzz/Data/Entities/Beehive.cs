using System.ComponentModel.DataAnnotations;

namespace BeeBuzz.Data.Entities
{
    public class Beehive
    {
        [Key]
        public int BeehiveId { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; }

        public string? DeactivationReason { get; set; }

        public ApplicationUser User { get; set; }
    }
}
