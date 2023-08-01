namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.GeneralApplicationConstants;


    [Area(AdminArea)]
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
        public async Task<IActionResult> Add(UserAddViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.AddUserAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            UserEditViewModel? model = await userService.GetEditUserModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserEditViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.EditUserAsync(id, user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
