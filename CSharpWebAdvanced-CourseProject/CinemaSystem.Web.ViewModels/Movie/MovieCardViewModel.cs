namespace CinemaSystem.Web.ViewModels.Movie
{
    public class MovieCardViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public IEnumerable<GenreViewModel> Genres { get; set; } = null!;

        public string? PosterUrl { get; set; }

    }
}
