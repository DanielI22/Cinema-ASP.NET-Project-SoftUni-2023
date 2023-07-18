namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Movie;
    using CinemaSystem.Web.ViewModels.Showtime;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ShowtimeService : IShowtimeService
    {
        private readonly CinemaSystemDbContext dbContext;
        private readonly IMovieService movieService;

        public ShowtimeService(CinemaSystemDbContext dbContext, IMovieService movieService)
        {
            this.dbContext = dbContext;
            this.movieService = movieService;
        }
        public async Task<IEnumerable<MovieShowtimeViewModel>> GetMovieShowtimesForCinemaDateAsync(int cinemaId, DateTime selectedDate)
        {
            List<Guid> movieIds = await dbContext.Showtimes
            .Where(s => s.CinemaId == cinemaId && s.StartTime.Date == selectedDate.Date)
            .Select(s => s.MovieId)
            .Distinct()
            .ToListAsync();

            IEnumerable<MovieCardViewModel> movieCards = await movieService.GetMovieCardsForMovieIdsAsync(movieIds);

            IEnumerable<MovieShowtimeViewModel> movieShowtimes = movieCards.Select(mc => new MovieShowtimeViewModel
            {
                MovieCard = mc,
                Showtimes = GetShowtimesForMovieAndCinemaAsync(mc.Id, cinemaId, selectedDate)
            }).ToList();


            return movieShowtimes;
        }

        private IEnumerable<ShowtimeViewModel> GetShowtimesForMovieAndCinemaAsync(Guid movieId, int cinemaId, DateTime date)
        {
            IEnumerable<ShowtimeViewModel> showtimes = dbContext.Showtimes
            .Where(s => s.MovieId == movieId && s.CinemaId == cinemaId && s.StartTime.Date == date.Date)
            .Select(s => new ShowtimeViewModel
            {
                Id = s.Id,
                TicketPrice = s.TicketPrice,
                StartTime = s.StartTime.ToString("hh:mm tt")
            })
            .ToList();
            return showtimes;
        }
    }
}
