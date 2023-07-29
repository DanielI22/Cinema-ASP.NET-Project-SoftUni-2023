namespace CinemaSystem.Web.ViewModels.Showtime
{
    using CinemaSystem.Web.ViewModels.Cinema;
    using CinemaSystem.Web.ViewModels.Movie;
    using System.ComponentModel.DataAnnotations;
    using static CinemaSystem.Common.EntityValidationConstants.Showtime;


    public class ShowtimeAddEditViewModel
    {
        [Required]
        [Display(Name = "Start time")]
        public DateTime StartTime { get; set; }

        [Required]
        [Range(TicketPriceMin, TicketPriceMax)]
        [Display(Name = "Ticket Price")]
        public decimal TicketPrice { get; set; }

        [Required]
        [Display(Name = "Movie")]
        public Guid MovieId { get; set; }

        [Required]
        public IEnumerable<ShowtimeMovieViewModel> Movies { get; set; } = new HashSet<ShowtimeMovieViewModel>();

        [Required]
        [Display(Name = "Cinema")]
        public int CinemaId { get; set; }

        [Required]
        public IEnumerable<CinemaViewModel> Cinemas { get; set; } = new HashSet<CinemaViewModel>();
    }
}
