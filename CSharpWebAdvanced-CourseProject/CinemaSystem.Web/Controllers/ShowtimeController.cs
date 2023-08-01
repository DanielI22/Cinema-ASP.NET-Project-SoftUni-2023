namespace CinemaSystem.Web.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Movie;
    using CinemaSystem.Web.ViewModels.Showtime;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.GeneralApplicationConstants;
    using static CinemaSystem.Common.NotificationMessagesConstants;


    [Authorize]
    public class ShowtimeController : Controller
    {
        private readonly IShowtimeService showtimeService;
        private readonly ICinemaService cinemaService;

        public ShowtimeController(IShowtimeService showtimeService, ICinemaService cinemaService)
        {
            this.showtimeService = showtimeService;
            this.cinemaService = cinemaService;
        }


        [AllowAnonymous]
        public async Task<IActionResult> Select(int cinemaId, string selectedDate)
        {
            if (cinemaId == 0)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction("Index", "Home");
            }
            try
            {
                IEnumerable<DateTime> dates = await cinemaService.GetCinemaAvailableDatesAsync(cinemaId);
                if (!dates.Any())
                {
                    TempData[ErrorMessage] = GeneralError;
                    return RedirectToAction("Index", "Home");
                }

                if (string.IsNullOrEmpty(selectedDate))
                {
                    selectedDate = dates.FirstOrDefault().ToString();
                }

                DateTime date = DateTime.Parse(selectedDate);
                IEnumerable<MovieShowtimeViewModel> movies = await showtimeService.GetMovieShowtimesForCinemaDateAsync(cinemaId, date);

                ShowtimeSelectViewModel viewModel = new ShowtimeSelectViewModel
                {
                    CinemaId = cinemaId,
                    SelectedDate = date.ToShortDateString(),
                    Movies = movies,
                    Dates = dates
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
