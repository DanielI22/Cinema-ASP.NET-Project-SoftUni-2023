namespace CinemaSystem.Web.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
	using System.Diagnostics;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMovieService movieService;

        public HomeController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<MovieCardViewModel> movieCards = await movieService.GetAllMoviesCardAsync();
            return View(movieCards);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return this.View("Error404");
            }
            return View();
        }
    }
}
