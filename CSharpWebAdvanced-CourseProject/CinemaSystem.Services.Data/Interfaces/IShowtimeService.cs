namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Movie;
    using CinemaSystem.Web.ViewModels.Showtime;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShowtimeService
    {
        Task AddShowtimeAsync(ShowtimeAddEditViewModel showtime);
        Task DeleteShowtimeAsync(string id);
        Task EditShowtimeAsync(string id, ShowtimeAddEditViewModel showtime);
        Task<ShowtimeAddEditViewModel?> GetEditShowtimeModelAsync(string id);
        Task<IEnumerable<MovieShowtimeViewModel>> GetMovieShowtimesForCinemaDateAsync(int cinemaId, DateTime selectedDate);
        Task<IEnumerable<ShowtimeDatailViewModel>> GetShowtimesAsync();
    }
}
