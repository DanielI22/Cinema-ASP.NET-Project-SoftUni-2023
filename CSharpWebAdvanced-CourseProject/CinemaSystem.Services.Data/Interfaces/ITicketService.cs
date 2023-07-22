namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Ticket;
    using System.Threading.Tasks;

    public interface ITicketService
    {
        Task<IEnumerable<int>> GetSelectedSeatsAsync(int showtimeId);
        Task ReserveTicketsAsync(int showtimeId, Guid usedId, List<int> selectedSeats);
    }
}
