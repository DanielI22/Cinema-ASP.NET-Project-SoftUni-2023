namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Movie;

    public interface IMovieService
    {
        Task<IEnumerable<MovieCardViewModel>> GetMovieCardsForMovieIdsAsync(List<string> movieIds);
        Task<IEnumerable<MovieCardViewModel>> GetAllMoviesCardAsync();

        Task<MovieDetailsViewModel?> GetMovieDetailsModelAsync(string id, int pageNumber, int pageSize);

        Task<IEnumerable<MovieCardViewModel>> FilterMoviesAsync(string? searchName, int selectedGenreId);
    }
}
