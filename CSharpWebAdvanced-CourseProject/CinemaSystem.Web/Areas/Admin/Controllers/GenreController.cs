namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Genre;
    using CinemaSystem.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.GeneralApplicationConstants;
    using static CinemaSystem.Common.NotificationMessagesConstants;



    [Area(AdminArea)]
    [Authorize(Roles = AdminRoleName)]
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<GenreViewModel> genres = await genreService.GetGenresAsync();
                return View(genres);
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
        public async Task<IActionResult> Add(GenreAddEditViewModel genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }

            try
            {
                await genreService.AddGenreAsync(genre);

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
                GenreAddEditViewModel? model = await genreService.GetEditGenreModelAsync(id);
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
        public async Task<IActionResult> Edit(string id, GenreAddEditViewModel genre)
        {
            if (id == null)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                return View(genre);
            }

            try
            {
                await genreService.EditGenreAsync(id, genre);
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
                await genreService.DeleteGenreAsync(id);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
