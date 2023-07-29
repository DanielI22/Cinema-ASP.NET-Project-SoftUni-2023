namespace CinemaSystem.Web.ViewModels.Ticket
{
    using CinemaSystem.Web.ViewModels.Cinema;
    using CinemaSystem.Web.ViewModels.Showtime;
    using System.ComponentModel.DataAnnotations;
    using static CinemaSystem.Common.EntityValidationConstants.Showtime;
    using static CinemaSystem.Common.EntityValidationConstants.Ticket;
    using System.Xml.Linq;

    public class TicketAddEditViewModel
    {

        [Required]
        [Range(TicketPriceMin, TicketPriceMax)]
        [Display(Name = "Ticket Price")]
        public decimal TicketPrice { get; set; }

        [Required]
        [Range(SeatNumberMin, SeatNumberMax)]
        [Display(Name = "Seat Number")]
        public int SeatNumber { get; set; }

        [Required]
        [Display(Name = "Showtime")]
        public int ShowtimeId { get; set; }

        [Required]
        public IDictionary<string, string> Showtimes { get; set; } = new Dictionary<string, string>();

        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; } = null!;

        [Required]
        public IDictionary<string, string> Users { get; set; } = new Dictionary<string, string>();
    }
}
