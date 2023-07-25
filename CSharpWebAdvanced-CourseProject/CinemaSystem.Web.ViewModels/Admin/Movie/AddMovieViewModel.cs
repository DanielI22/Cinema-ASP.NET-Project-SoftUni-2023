namespace CinemaSystem.Web.ViewModels.Admin.Movie
{
    using CinemaSystem.Web.ViewModels.Movie;
    using System.ComponentModel.DataAnnotations;
    using static CinemaSystem.Common.EntityValidationConstants.Movie;

    public class AddMovieViewModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [Range(ReleaseYearMin, ReleaseYearMax)]
        public int ReleaseYear { get; set; }

        [MaxLength(PosterImageUrlMaxLength)]
        public string? PosterImageUrl { get; set; }

        [Required]
        public List<int> GenresId { get; set; } = null!;

        [Required]
        public IEnumerable<GenreViewModel> Genres { get; set; } = new HashSet<GenreViewModel>();
    }
}
