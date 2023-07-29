namespace CinemaSystem.Web.ViewModels.Showtime
{
    public class ShowtimeDatailViewModel
    {
        public string Id { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public decimal TicketPrice { get; set; }
        public string MovieName { get; set; } = null!;
        public string CinemaName { get; set; } = null!;
    }
}
