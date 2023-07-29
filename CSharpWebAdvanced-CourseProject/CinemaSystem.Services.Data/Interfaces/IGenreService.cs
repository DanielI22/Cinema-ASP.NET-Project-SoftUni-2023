namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Genre;
    using CinemaSystem.Web.ViewModels.Movie;

    public interface IGenreService
    {
        Task AddGenreAsync(GenreAddEditViewModel genre);
        Task DeleteGenreAsync(string id);
        Task EditGenreAsync(string id, GenreAddEditViewModel genre);
        Task<GenreAddEditViewModel?> GetEditGenreModelAsync(string id);
        Task<IEnumerable<GenreViewModel>> GetGenresAsync();
    }
}
