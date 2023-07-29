namespace CinemaSystem.Web.ViewModels.Showtime
{
    using System.ComponentModel.DataAnnotations;

    public class ShowtimeViewModel
    {
        public string Id { get; set; } = null!;
        public string StartTime { get; set; } = null!;
        public decimal TicketPrice { get; set; }
    }
}
