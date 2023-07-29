﻿namespace CinemaSystem.Web.Areas.Admin.Controllers
{
    using CinemaSystem.Services.Data;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Movie;
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
        public async Task<IActionResult> Index()
        {
            IEnumerable<MovieShowViewModel> movies = await movieService.GetAllMoviesAsync();
            return View(movies);
        }

        public async Task<IActionResult> Add()
        {
            var model = new MovieAddEditViewModel();
            model.Genres = await genreService.GetGenresAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MovieAddEditViewModel movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            await movieService.AddMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            MovieAddEditViewModel? model = await movieService.GetEditMovieModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, MovieAddEditViewModel movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            await movieService.EditMovieAsync(id, movie);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await movieService.DeleteMovieAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
