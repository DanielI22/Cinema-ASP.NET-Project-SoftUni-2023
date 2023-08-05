namespace CinemaSystem.Web.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.GeneralApplicationConstants;
    using static CinemaSystem.Common.NotificationMessagesConstants;

    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;

        public MovieController(IMovieService movieService, IGenreService genreService)
        {
            this.movieService = movieService;
            this.genreService = genreService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(string id, int pageNumber = 1, int pageSize = ReviewsPerPage)
        {
            if (id == null)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction("Index", "Home");
            }
            try
            {
                MovieDetailsViewModel? model = await movieService.GetMovieDetailsModelAsync(id, pageNumber, pageSize);
                if (model == null)
                {
                    TempData[ErrorMessage] = "Your Movie could not be found!";
                    return RedirectToAction("Index", "Home");
                }

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> All([FromQuery] MoviesViewModel moviesViewModel)
        {
            try
            {
                IEnumerable<MovieCardViewModel> movies = await movieService.FilterMoviesAsync(moviesViewModel.SearchName, moviesViewModel.SelectedGenreId);
                IEnumerable<GenreViewModel> genres = await genreService.GetGenresAsync();
                var viewModel = new MoviesViewModel
                {
                    Movies = movies,
                    Genres = genres,
                    SearchName = moviesViewModel.SearchName,
                    SelectedGenreId = moviesViewModel.SelectedGenreId
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction("All", "Movie");
            }
        }

        [AllowAnonymous]
        public IActionResult Clear()
        {
            return RedirectToAction(nameof(All));
        }

    }
}
