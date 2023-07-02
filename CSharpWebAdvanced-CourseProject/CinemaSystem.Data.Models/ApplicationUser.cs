using Microsoft.AspNetCore.Identity;

namespace CinemaSystem.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            ReservedTickets = new HashSet<Ticket>();
            Reviews = new HashSet<Review>();
        }
        public virtual ICollection<Ticket> ReservedTickets { get; set; } = null!;
        public virtual ICollection<Review> Reviews { get; set; } = null!;
    }
}
