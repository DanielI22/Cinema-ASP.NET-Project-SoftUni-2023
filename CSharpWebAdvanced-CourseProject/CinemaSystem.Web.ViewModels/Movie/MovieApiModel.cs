namespace CinemaSystem.Web.ViewModels.Movie
{

    public class MovieApiModel
    {
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Year { get; set; } = null!;
        public string? Plot { get; set; }
        public string? Poster { get; set; }
    }
}
