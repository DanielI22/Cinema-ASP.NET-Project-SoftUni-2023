﻿namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Ticket;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.GeneralApplicationConstants;


    [Area("Admin")]
    [Authorize(Roles = AdminRoleName)]
    public class UserController : Controller
    {
        private readonly IUserService userService;


        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserViewModel> users = await userService.GetUsersAsync();
            return View(users);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddEditViewModel ticket)
        {
            if (!ModelState.IsValid)
            {
                return View(ticket);
            }

            await userService.AddUserAsync(ticket);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            UserAddEditViewModel? model = await userService.GetEditUserModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, TicketAddEditViewModel ticket)
        {
            if (!ModelState.IsValid)
            {
                return View(ticket);
            }

            await userService.EditUserAsync(id, ticket);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
