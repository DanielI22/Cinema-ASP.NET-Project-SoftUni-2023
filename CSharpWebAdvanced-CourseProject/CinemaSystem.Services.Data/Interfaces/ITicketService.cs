namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Ticket;
    using System.Threading.Tasks;

    public interface ITicketService
    {
        Task AddTicketAsync(TicketAddEditViewModel ticket);
        Task DeleteTicketAsync(string id);
        Task EditTicketAsync(string id, TicketAddEditViewModel ticket);
        Task<TicketAddEditViewModel?> GetAddTicketModelAsync();
        Task<TicketAddEditViewModel?> GetEditTicketModelAsync(string id);
        Task<IEnumerable<int>> GetSelectedSeatsAsync(string showtimeId);
        Task<IEnumerable<TicketViewModel>> GetTicketsAsync();
        Task ReserveTicketsAsync(string showtimeId, string usedId, List<int> selectedSeats);
    }
}
