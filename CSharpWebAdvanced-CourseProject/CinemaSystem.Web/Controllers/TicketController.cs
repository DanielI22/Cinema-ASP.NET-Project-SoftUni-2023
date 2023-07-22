namespace CinemaSystem.Web.Controllers
{
    using CinemaSystem.Common;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Infrastructure.Extensions;
    using CinemaSystem.Web.ViewModels.Showtime;
    using CinemaSystem.Web.ViewModels.Ticket;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.NotificationMessagesConstants;
    using static CinemaSystem.Common.Utils;


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
            TicketSelectionViewModel ticketSelectionViewModel = new()
            {
                ShowtimeId = showtimeId
            };
            ticketSelectionViewModel.ReservedSeats = await ticketService.GetSelectedSeatsAsync(ticketSelectionViewModel.ShowtimeId);

            return View(ticketSelectionViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Reserve(int showtimeId, string selectedSeats)
        {
            try
            {
                List<int> selectedSeatsNumbers = ParseCommaSeparatedString(selectedSeats);
                string? userId = User.GetId();
                if(userId != null)
                {
                    await ticketService.ReserveTicketsAsync(showtimeId, userId, selectedSeatsNumbers);
                }
                else
                {
                    TempData[ErrorMessage] = "There was an error!";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "There was an error!";
                return RedirectToAction("Index", "Home");
            }

            TempData[SuccessMessage] = "You reservation is completed. We are expecting you!";
            return RedirectToAction("Index", "Home");
        }
    }
}
