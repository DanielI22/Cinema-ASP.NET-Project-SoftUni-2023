namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Movie;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShowtimeService
    {
        Task<IEnumerable<MovieShowtimeViewModel>> GetMovieShowtimesForCinemaDateAsync(int cinemaId, DateTime selectedDate);
    }
}
