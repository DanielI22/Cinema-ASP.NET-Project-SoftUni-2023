namespace CinemaSystem.Web.ViewModels.Home
{
    public class MovieCardViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string? PosterUrl { get; set; }

    }
}
