namespace CinemaSystem.Web.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Showtime;
    using CinemaSystem.Web.ViewModels.Ticket;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class TicketController : Controller
    {

        private readonly ITicketService ticketService;

        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        public async Task<IActionResult> Reserve(int showtimeId)
        {
            TicketSelectionViewModel ticketSelectionViewModel = new TicketSelectionViewModel();
            ticketSelectionViewModel.ShowtimeId = showtimeId;

            return View(ticketSelectionViewModel);
        }
    }
}
