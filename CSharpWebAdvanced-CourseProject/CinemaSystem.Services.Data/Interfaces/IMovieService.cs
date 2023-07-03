namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Home;

    public interface IMovieService
    {
        Task<IEnumerable<MovieCardViewModel>> GetAllMoviesCardAsync();
    }
}
