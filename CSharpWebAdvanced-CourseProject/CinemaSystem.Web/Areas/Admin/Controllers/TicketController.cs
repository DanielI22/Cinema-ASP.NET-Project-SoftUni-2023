namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Ticket;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.GeneralApplicationConstants;
    using static CinemaSystem.Common.NotificationMessagesConstants;


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
            try
            {
                IEnumerable<TicketViewModel> tickets = await ticketService.GetTicketsAsync();
                return View(tickets);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction("Dashboard", "Admin");
            }
        }

        public async Task<IActionResult> Add()
        {
            try
            {
                var model = await ticketService.GetAddTicketModelAsync();
                return View(model);
            }
            catch
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction("Dashboard", "Admin");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(TicketAddEditViewModel ticket)
        {
            if (!ModelState.IsValid)
            {
                return View(ticket);
            }
            try
            {

                await ticketService.AddTicketAsync(ticket);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction(nameof(Index));
            }
            try
            {
                TicketAddEditViewModel? model = await ticketService.GetEditTicketModelAsync(id);
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    TempData[ErrorMessage] = GeneralError;
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, TicketAddEditViewModel ticket)
        {
            if (id == null)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                return View(ticket);
            }
            try
            {
                await ticketService.EditTicketAsync(id, ticket);

            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction(nameof(Index));
            }
            try
            {
                await ticketService.DeleteTicketAsync(id);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
