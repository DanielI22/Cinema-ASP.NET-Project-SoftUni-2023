namespace CinemaSystem.Web.ViewModels.Movie
{
    using CinemaSystem.Web.ViewModels.Showtime;

    public class MovieShowtimeViewModel
    {
        public MovieCardViewModel? MovieCard { get; set; }
        public IEnumerable<ShowtimeViewModel> Showtimes { get; set; } = new List<ShowtimeViewModel>();
    }
}
