namespace CinemaSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using static CinemaSystem.Common.EntityValidationConstants.User;


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
