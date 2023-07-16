namespace CinemaSystem.Web.ViewModels.Showtime
{
    using CinemaSystem.Web.ViewModels.Movie;

    public class ShowtimeSelectViewModel
    {
        public int CinemaId { get; set; }
        public IEnumerable<DateTime> Dates { get; set; } = new List<DateTime>();
        public string SelectedDate { get; set; } = null!;
        public IEnumerable<MovieShowtimeViewModel> Movies { get; set; } = new List<MovieShowtimeViewModel>();
    }
}
