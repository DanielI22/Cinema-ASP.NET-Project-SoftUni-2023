namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Genre;
    using CinemaSystem.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.GeneralApplicationConstants;


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
            IEnumerable<GenreViewModel> genres = await genreService.GetGenresAsync();
            return View(genres);
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

            await genreService.AddGenreAsync(genre);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            GenreAddEditViewModel? model = await genreService.GetEditGenreModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, GenreAddEditViewModel genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }

            await genreService.EditGenreAsync(id, genre);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await genreService.DeleteGenreAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
