namespace CinemaSystem.Web.ViewModels.Review
{
    public class ReviewViewModel
    {
        public Guid ReviewId { get; set; }

        public Guid CreatorId { get; set; }

        public string ReviewText { get; set; } = null!;

        public string ReviewAuthor { get; set; } = null!;
    }
}
