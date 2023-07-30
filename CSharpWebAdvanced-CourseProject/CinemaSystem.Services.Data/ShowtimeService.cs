namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Cinema;
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

        public async Task AddShowtimeAsync(ShowtimeAddEditViewModel model)
        {
            Showtime showtime = new Showtime
            {
                CinemaId = model.CinemaId,
                MovieId = model.MovieId,
                TicketPrice = model.TicketPrice,
                StartTime = model.StartTime
            };

            await dbContext.Showtimes.AddAsync(showtime);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteShowtimeAsync(string id)
        {
            var showtime = await dbContext.Showtimes
            .FirstOrDefaultAsync(sh => sh.Id.ToString() == id);

            if (showtime != null)
            {
                showtime.isActive = false;
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task EditShowtimeAsync(string id, ShowtimeAddEditViewModel model)
        {
            var showtime = await dbContext.Showtimes
                .Where(sh => sh.isActive)
                .FirstOrDefaultAsync(sh => sh.Id.ToString() == id);

            if (showtime != null)
            {
                showtime.TicketPrice = model.TicketPrice;
                showtime.CinemaId = model.CinemaId;
                showtime.MovieId = model.MovieId;
                showtime.StartTime = model.StartTime;

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<ShowtimeAddEditViewModel?> GetEditShowtimeModelAsync(string id)
        {
            var cinemas = await dbContext.Cinemas
            .Where(c => c.isActive)
            .Select(c => new CinemaViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();

            var movies = await dbContext.Movies
            .Where(m => m.isActive)
            .Select(m => new ShowtimeMovieViewModel
            {
                Id = m.Id,
                Title = m.Title
            }).ToListAsync();

            return await dbContext.Showtimes
                .Where(sh => sh.Id.ToString() == id)
                .Select(sh => new ShowtimeAddEditViewModel
                {
                    TicketPrice = sh.TicketPrice,
                    StartTime = sh.StartTime,
                    MovieId = sh.MovieId,
                    CinemaId = sh.CinemaId,
                    Cinemas = cinemas,
                    Movies = movies
                }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MovieShowtimeViewModel>> GetMovieShowtimesForCinemaDateAsync(int cinemaId, DateTime selectedDate)
        {
            List<string> movieIds = await dbContext.Showtimes
            .Where(s => s.CinemaId == cinemaId && s.StartTime.Date == selectedDate.Date && s.isActive)
            .Select(s => s.MovieId.ToString())
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

        public async Task<IEnumerable<ShowtimeDatailViewModel>> GetShowtimesAsync()
        {
            return await dbContext.Showtimes
              .Where(sh => sh.isActive)
              .OrderBy(sh => sh.StartTime)
              .Select(sh => new ShowtimeDatailViewModel
              {
                  Id = sh.Id.ToString(),
                  TicketPrice = sh.TicketPrice,
                  StartTime = sh.StartTime,
                  CinemaName = sh.Cinema.Name,
                  MovieName = sh.Movie.Title
              }).ToListAsync();
        }

        private IEnumerable<ShowtimeViewModel> GetShowtimesForMovieAndCinemaAsync(Guid movieId, int cinemaId, DateTime date)
        {
            IEnumerable<ShowtimeViewModel> showtimes = dbContext.Showtimes
            .Where(s => s.MovieId == movieId && s.CinemaId == cinemaId && s.StartTime.Date == date.Date && s.isActive)
            .Select(s => new ShowtimeViewModel
            {
                Id = s.Id.ToString(),
                TicketPrice = s.TicketPrice,
                StartTime = s.StartTime.ToString("HH:mm")
            })
            .ToList();
            return showtimes;
        }
    }
}
