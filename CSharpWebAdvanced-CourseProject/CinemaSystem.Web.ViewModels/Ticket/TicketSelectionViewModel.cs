namespace CinemaSystem.Web.ViewModels.Ticket
{
    public class TicketSelectionViewModel
    {
        public string ShowtimeId { get; set; } = null!;
        public IEnumerable<int> ReservedSeats { get; set; } = new List<int>();
    }
}
