namespace CinemaSystem.Web.ViewModels.Movie
{
    using System.ComponentModel.DataAnnotations;
    using CinemaSystem.Web.ViewModels.Genre;
    using static CinemaSystem.Common.EntityValidationConstants.Movie;

    public class MovieAddEditViewModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(ReleaseYearMin, ReleaseYearMax)]
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; } = 1900;

        [MaxLength(PosterImageUrlMaxLength)]
        public string? PosterImageUrl { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public List<int> GenresId { get; set; } = null!;

        [Required]
        public IEnumerable<GenreViewModel> Genres { get; set; } = new HashSet<GenreViewModel>();
    }
}
