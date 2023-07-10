namespace CinemaSystem.Web.ViewModels.Movie
{
    using CinemaSystem.Web.ViewModels.Home;

    public class MoviesViewModel
    {
        public IEnumerable<MovieCardViewModel> Movies { get; set; } = null!;
        public IEnumerable<GenreViewModel> Genres { get; set; } = null!;
        public string? SearchName { get; set; }
        public int SelectedGenreId { get; set; }
    }
}
