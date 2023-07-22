namespace CinemaSystem.Web.ViewModels.Ticket
{
    using CinemaSystem.Web.ViewModels.Movie;

    public class UserTicketViewModel
    {
        public MovieCardViewModel Movie { get; set; } = null!;
        public decimal TicketPrice { get; set; }
        public string SeatNumber { get; set; } = null!;
        public DateTime ShowtimeStartTime { get; set; }
        public string CinemaName { get; set; } = null!;
    }
}
