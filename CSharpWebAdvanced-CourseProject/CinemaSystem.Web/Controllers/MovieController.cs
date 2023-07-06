namespace CinemaSystem.Web.Controllers
{
    using CinemaSystem.Services.Data;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.NotificationMessagesConstants;


    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            MovieDetailsViewModel? model = await movieService.GetNewMovieDetailsModelAsync(id);
            if (model == null)
            {
                TempData[ErrorMessage] = "Your Movie could not be found!";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
