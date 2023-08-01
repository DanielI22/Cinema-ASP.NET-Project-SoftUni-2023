namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Cinema;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.GeneralApplicationConstants;


    [Area(AdminArea)]
    [Authorize(Roles = AdminRoleName)]
    public class CinemaController : Controller
    {
        private readonly ICinemaService cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
        }

        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CinemaViewModel> cinemas = await cinemaService.GetAllCinemasAsync();
            return View(cinemas);
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

            await cinemaService.AddCinemaAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            CinemaAddEditViewModel? model = await cinemaService.GetEditCinemaModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CinemaAddEditViewModel cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }

            await cinemaService.EditCinemaAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await cinemaService.DeleteCinemaAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
