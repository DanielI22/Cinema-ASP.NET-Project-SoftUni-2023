namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Ticket;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.GeneralApplicationConstants;


    [Area(AdminArea)]
    [Authorize(Roles = AdminRoleName)]
    public class TicketController : Controller
    {
        private readonly ITicketService ticketService;


        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<TicketViewModel> tickets = await ticketService.GetTicketsAsync();
            return View(tickets);
        }

        public async Task<IActionResult> Add()
        {
            var model = await ticketService.GetAddTicketModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TicketAddEditViewModel ticket)
        {
            if (!ModelState.IsValid)
            {
                return View(ticket);
            }

            await ticketService.AddTicketAsync(ticket);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            TicketAddEditViewModel? model = await ticketService.GetEditTicketModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, TicketAddEditViewModel ticket)
        {
            if (!ModelState.IsValid)
            {
                return View(ticket);
            }

            await ticketService.EditTicketAsync(id, ticket);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await ticketService.DeleteTicketAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
