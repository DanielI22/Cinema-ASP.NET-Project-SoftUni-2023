namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Home;
    using CinemaSystem.Web.ViewModels.Movie;

    public interface IMovieService
    {
        Task<IEnumerable<MovieCardViewModel>> GetAllMoviesCardAsync();
        Task<MovieDetailsViewModel?> GetNewMovieDetailsModelAsync(Guid id);
    }
}
