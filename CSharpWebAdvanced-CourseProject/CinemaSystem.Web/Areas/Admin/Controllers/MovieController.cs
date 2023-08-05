namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using static CinemaSystem.Common.GeneralApplicationConstants;
    using static CinemaSystem.Common.NotificationMessagesConstants;

    [Area(AdminArea)]
    [Authorize(Roles = AdminRoleName)]
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;

        public MovieController(IMovieService movieService, IGenreService genreService)
        {
            this.movieService = movieService;
            this.genreService = genreService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<MovieCardViewModel> movies = await movieService.GetAllMoviesCardAsync();
                return View(movies);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction("Dashboard", "Admin");
            }
        }

        public async Task<IActionResult> Add()
        {
            var model = new MovieAddEditViewModel();
            try
            {
                model.Genres = await genreService.GetGenresAsync();
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction("Dashboard", "Admin");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MovieAddEditViewModel movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            try
            {
                await movieService.AddMovieAsync(movie);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddTitle(string title, int year)
        {
            if (title == null)
            {
                return RedirectToAction(nameof(Add));
            }
            try
            {
                await movieService.AddMovieApiTitleAsync(title, year);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Incorrect movie identifiers";
                return RedirectToAction(nameof(Add));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddId(string imdbTag)
        {
            if (imdbTag == null)
            {
                return RedirectToAction(nameof(Add));
            }
            try
            {
                await movieService.AddMovieApiIdAsync(imdbTag);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Incorrect movie identifiers";
                return RedirectToAction(nameof(Add));
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
                MovieAddEditViewModel? model = await movieService.GetEditMovieModelAsync(id);
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
        public async Task<IActionResult> Edit(string id, MovieAddEditViewModel movie)
        {
            if (id == null)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            try
            {

                await movieService.EditMovieAsync(id, movie);
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
                await movieService.DeleteMovieAsync(id);

            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
