namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.GeneralApplicationConstants;
    using static CinemaSystem.Common.NotificationMessagesConstants;


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
            try
            {
                IEnumerable<UserViewModel> users = await userService.GetUsersAsync();
                return View(users);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction("Dashboard", "Admin");
            }
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
                try
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
                catch (Exception)
                {
                    TempData[ErrorMessage] = GeneralError;
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(user);
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
                UserEditViewModel? model = await userService.GetEditUserModelAsync(id);
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
        public async Task<IActionResult> Edit(string id, UserEditViewModel user)
        {
            if (id == null)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                try
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
                catch (Exception)
                {
                    TempData[ErrorMessage] = GeneralError;
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(user);
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
                await userService.DeleteUserAsync(id);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
