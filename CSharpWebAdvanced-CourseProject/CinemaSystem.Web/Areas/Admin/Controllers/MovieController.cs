namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Services.Data;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Admin.Movie;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using static CinemaSystem.Common.GeneralApplicationConstants;

    [Area("Admin")]
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
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<MovieShowViewModel> movies = await movieService.GetAllMoviesAsync();
            return View(movies);
        }

        public async Task<IActionResult> Add()
        {
            var model = new AddMovieViewModel();
            model.Genres = await genreService.GetGenresAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            await movieService.AddMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Edit(Guid id)
        //{
        //    Movie movie = await movieService.GetMovieByIdAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(movie);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(Movie movie)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(movie);
        //    }

        //    await movieService.UpdateMovieAsync(movie);
        //    return RedirectToAction(nameof(Index));
        //}

        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    Movie movie = await movieService.GetMovieByIdAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(movie);
        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    await movieService.DeleteMovieAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
