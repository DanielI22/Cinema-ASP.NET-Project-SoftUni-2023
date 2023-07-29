namespace CinemaSystem.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Showtime
    {
        public Showtime()
        {
            Tickets = new HashSet<Ticket>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        [Precision(5, 2)]
        public decimal TicketPrice { get; set; }

        [Required]
        public Guid MovieId { get; set; }

        [ForeignKey(nameof(MovieId))]
        public virtual Movie Movie { get; set; } = null!;

        [Required]
        public int CinemaId { get; set; }

        [ForeignKey(nameof(CinemaId))]
        public virtual Cinema Cinema { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; }

        public bool isActive { get; set; } = true;

    }
}
