namespace CinemaSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore;

    public class Ticket
    {
        public Ticket()
        {
            Id = Guid.NewGuid();    
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [Precision(5, 2)]
        public decimal Price { get; set; }

        [Required]
        public string SeatNumber { get; set; } = null!;

        [Required]
        public int ShowtimeId { get; set; }

        [ForeignKey(nameof(ShowtimeId))]
        public Showtime Showtime { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        public bool isActive { get; set; } = true;

    }
}
