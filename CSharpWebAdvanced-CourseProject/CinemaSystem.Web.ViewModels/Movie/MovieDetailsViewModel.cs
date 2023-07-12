namespace CinemaSystem.Web.ViewModels.Movie
{
    using CinemaSystem.Web.ViewModels.Review;
    using System.ComponentModel.DataAnnotations;
    using static CinemaSystem.Common.EntityValidationConstants.Review;
    public class MovieDetailsViewModel
    {
        public Guid Id { get; set; }

        public string? PosterUrl { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int ReleaseYear { get; set; }

        public IEnumerable<string> Genres { get; set; } = new List<string>();

        public int TotalReviews { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalReviews / PageSize);

        public IEnumerable<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();

        [Required]
        [MinLength(TextMinLength)]
        [MaxLength(TextMaxLength)]
        [Display(Name = "Review Text")]
        public string ReviewToAdd { get; set; } = null!;

    }
}
