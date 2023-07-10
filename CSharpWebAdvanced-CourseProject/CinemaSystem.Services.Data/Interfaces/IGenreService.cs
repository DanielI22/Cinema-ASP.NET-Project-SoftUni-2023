namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Movie;

    public interface IGenreService
    {
        Task<IEnumerable<GenreViewModel>> GetGenresAsync();
    }
}
