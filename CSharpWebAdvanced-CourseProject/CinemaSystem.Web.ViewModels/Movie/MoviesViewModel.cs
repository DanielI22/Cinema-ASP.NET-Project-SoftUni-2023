using CinemaSystem.Web.ViewModels.Genre;

namespace CinemaSystem.Web.ViewModels.Movie
{
    public class MoviesViewModel
    {
        public IEnumerable<MovieCardViewModel> Movies { get; set; } = null!;
        public IEnumerable<GenreViewModel> Genres { get; set; } = null!;
        public string? SearchName { get; set; }
        public int SelectedGenreId { get; set; }
    }
}
