namespace CinemaSystem.Web.ViewModels.Movie
{
    public class MovieShowViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int ReleaseYear { get; set; }

        public string? PosterImageUrl { get; set; }

        public IEnumerable<GenreViewModel>? Genres { get; set; }
    }
}
