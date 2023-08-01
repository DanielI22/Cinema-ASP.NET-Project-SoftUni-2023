namespace CinemaSystem.Web.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Movie;
    using CinemaSystem.Web.ViewModels.Showtime;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
            ShowtimeSelectViewModel viewModel;
            IEnumerable<DateTime> dates = await cinemaService.GetCinemaAvailableDatesAsync(cinemaId);

            if (string.IsNullOrEmpty(selectedDate))
            {
                selectedDate = dates.FirstOrDefault().ToString();
            }

            DateTime date = DateTime.Parse(selectedDate);
            IEnumerable<MovieShowtimeViewModel> movies = await showtimeService.GetMovieShowtimesForCinemaDateAsync(cinemaId, date);

            viewModel = new ShowtimeSelectViewModel
            {
                CinemaId = cinemaId,
                SelectedDate = date.ToShortDateString(),
                Movies = movies,
                Dates = dates
            };

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Select(int cinemaId, ShowtimeSelectViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(viewModel.SelectedDate))
            {
                DateTime selectedDate = DateTime.Parse(viewModel.SelectedDate);
                IEnumerable<MovieShowtimeViewModel> movies = await showtimeService.GetMovieShowtimesForCinemaDateAsync(cinemaId, selectedDate);

                viewModel.Movies = movies;
                viewModel.SelectedDate = selectedDate.ToShortDateString();

                return RedirectToAction("Select", new { cinemaId, selectedDate = viewModel.SelectedDate });
            }

            IEnumerable<DateTime> dates = await cinemaService.GetCinemaAvailableDatesAsync(cinemaId);
            viewModel.Dates = dates;

            return View(viewModel);
        }
    }
}
