namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Genre;
    using CinemaSystem.Web.ViewModels.Movie;
    using CinemaSystem.Web.ViewModels.Showtime;
    using CinemaSystem.Web.ViewModels.Ticket;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Data;
    using static CinemaSystem.Common.GeneralApplicationConstants;


    [Area("Admin")]
    [Authorize(Roles = AdminRoleName)]
    public class TicketController : Controller
    {
        private readonly ITicketService ticketService;
        private readonly IShowtimeService showtimeService;
        private readonly UserManager<ApplicationUser> userManager;


        public TicketController(ITicketService ticketService, UserManager<ApplicationUser> userManager, IShowtimeService showtimeService)
        {
            this.ticketService = ticketService;
            this.userManager = userManager;
            this.showtimeService = showtimeService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<TicketViewModel> tickets = await ticketService.GetTicketsAsync();
            return View(tickets);
        }

        public async Task<IActionResult> Add()
        {
            var model = new TicketAddEditViewModel();
            var showtimes = await showtimeService.GetShowtimesAsync();
            var allUsers = await userManager.Users.ToListAsync();
            model.Showtimes = showtimes.ToDictionary(s => s.Id, s => s.CinemaName + " - " + s.MovieName + " - " + s.StartTime.ToString());
            model.Users = allUsers.ToDictionary(user => user.Id.ToString(), user => user.UserName);
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
