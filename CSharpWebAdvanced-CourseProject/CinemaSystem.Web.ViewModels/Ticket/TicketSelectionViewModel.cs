namespace CinemaSystem.Web.ViewModels.Ticket
{
    public class TicketSelectionViewModel
    {
        public int ShowtimeId { get; set; }
        public IEnumerable<int> ReservedSeats { get; set; } = new List<int>();
    }
}
