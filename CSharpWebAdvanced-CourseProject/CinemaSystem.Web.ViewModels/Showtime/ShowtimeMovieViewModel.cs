namespace CinemaSystem.Web.ViewModels.Showtime
{
    using System.ComponentModel.DataAnnotations;

    public class ShowtimeMovieViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

    }
}
