namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Movie;
    using CinemaSystem.Web.ViewModels.Showtime;
    using System;

    public interface IMovieService
    {
        Task<IEnumerable<MovieShowViewModel>> GetAllMoviesAsync();
        Task<IEnumerable<ShowtimeMovieViewModel>> GetAllShowtimeMoviesAsync();
        Task<IEnumerable<MovieCardViewModel>> GetMovieCardsForMovieIdsAsync(List<string> movieIds);

        Task<IEnumerable<MovieCardViewModel>> GetAllMoviesCardAsync();

        Task<MovieDetailsViewModel?> GetMovieDetailsModelAsync(string id, int pageNumber, int pageSize);

        Task<IEnumerable<MovieCardViewModel>> FilterMoviesAsync(string? searchName, int selectedGenreId);
        Task AddMovieAsync(MovieAddEditViewModel movie);
        Task<MovieAddEditViewModel?> GetEditMovieModelAsync(string id);
        Task EditMovieAsync(string id, MovieAddEditViewModel movie);
        Task DeleteMovieAsync(string id);
    }
}
