namespace CinemaSystem.Web.ViewModels.Ticket
{
    public class TicketViewModel
    {
        public string Id { get; set; } = null!;
        public string SeatNumber { get; set; } = null!;
        public decimal Price { get; set; }
        public string Showtime { get; set; } = null!;
        public string Username { get; set; } = null!;

    }
}
