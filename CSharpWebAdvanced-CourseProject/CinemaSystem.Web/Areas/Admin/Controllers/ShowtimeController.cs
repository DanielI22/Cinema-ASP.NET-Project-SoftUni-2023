namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Showtime;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.GeneralApplicationConstants;
    using static CinemaSystem.Common.NotificationMessagesConstants;


    [Area(AdminArea)]
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
            try
            {
                IEnumerable<ShowtimeDatailViewModel> showtimes = await showtimeService.GetShowtimesAsync();
                return View(showtimes);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction("Dashboard", "Admin");
            }
        }

        public async Task<IActionResult> Add()
        {
            var model = new ShowtimeAddEditViewModel();
            try
            {
                model.Movies = await movieService.GetAllShowtimeMoviesAsync();
                model.Cinemas = await cinemaService.GetAllCinemasAsync();
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction("Dashboard", "Admin");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ShowtimeAddEditViewModel showtime)
        {
            if (!ModelState.IsValid)
            {
                return View(showtime);
            }
            try
            {
                await showtimeService.AddShowtimeAsync(showtime);
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
                ShowtimeAddEditViewModel? model = await showtimeService.GetEditShowtimeModelAsync(id);
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
        public async Task<IActionResult> Edit(string id, ShowtimeAddEditViewModel showtime)
        {
            if (id == null)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                return View(showtime);
            }
            try
            {

                await showtimeService.EditShowtimeAsync(id, showtime);
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

                await showtimeService.DeleteShowtimeAsync(id);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
