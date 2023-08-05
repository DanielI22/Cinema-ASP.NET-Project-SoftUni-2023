namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Cinema;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.GeneralApplicationConstants;
    using static CinemaSystem.Common.NotificationMessagesConstants;


    [Area(AdminArea)]
    [Authorize(Roles = AdminRoleName)]
    public class CinemaController : Controller
    {
        private readonly ICinemaService cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<CinemaViewModel> cinemas = await cinemaService.GetAllCinemasAsync();
                return View(cinemas);
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
        public async Task<IActionResult> Add(CinemaAddEditViewModel cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }

            try
            {
                await cinemaService.AddCinemaAsync(cinema);
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
                CinemaAddEditViewModel? model = await cinemaService.GetEditCinemaModelAsync(id);
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
        public async Task<IActionResult> Edit(string id, CinemaAddEditViewModel cinema)
        {
            if (id == null)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }

            try
            {
                await cinemaService.EditCinemaAsync(id, cinema);
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
                await cinemaService.DeleteCinemaAsync(id);

            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
