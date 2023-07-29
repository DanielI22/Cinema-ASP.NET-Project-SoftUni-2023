namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Movie;
    using CinemaSystem.Web.ViewModels.Showtime;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Data;
    using static CinemaSystem.Common.GeneralApplicationConstants;


    [Area("Admin")]
    [Authorize(Roles = AdminRoleName)]
    public class ShowtimeController : Controller
    {
        private readonly IShowtimeService showtimeService;
        private readonly ICinemaService cinemaService;
        private readonly IMovieService movieService;


        public ShowtimeController(IShowtimeService showtimeService, ICinemaService cinemaService, IMovieService movieService)
        {
            this.showtimeService = showtimeService;
            this.cinemaService = cinemaService;
            this.movieService = movieService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<ShowtimeDatailViewModel> showtimes = await showtimeService.GetShowtimesAsync();
            return View(showtimes);
        }

        public async Task<IActionResult> Add()
        {
            var model = new ShowtimeAddEditViewModel();
            model.Movies = await movieService.GetAllShowtimeMoviesAsync();
            model.Cinemas = await cinemaService.GetAllCinemasAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ShowtimeAddEditViewModel showtime)
        {
            if (!ModelState.IsValid)
            {
                return View(showtime);
            }

            await showtimeService.AddShowtimeAsync(showtime);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            ShowtimeAddEditViewModel? model = await showtimeService.GetEditShowtimeModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ShowtimeAddEditViewModel showtime)
        {
            if (!ModelState.IsValid)
            {
                return View(showtime);
            }

            await showtimeService.EditShowtimeAsync(id, showtime);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await showtimeService.DeleteShowtimeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
