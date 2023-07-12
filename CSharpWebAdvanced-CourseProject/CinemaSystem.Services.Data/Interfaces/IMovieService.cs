namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Home;
    using CinemaSystem.Web.ViewModels.Movie;

    public interface IMovieService
    {
        Task<IEnumerable<MovieCardViewModel>> GetAllMoviesCardAsync();

        Task<MovieDetailsViewModel?> GetMovieDetailsModelAsync(Guid id, int pageNumber, int pageSize);

        Task<IEnumerable<MovieCardViewModel>> FilterMoviesAsync(string? searchName, int selectedGenreId);
    }
}
