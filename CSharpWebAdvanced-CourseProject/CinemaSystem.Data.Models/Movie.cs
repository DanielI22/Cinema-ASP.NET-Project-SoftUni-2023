namespace CinemaSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static CinemaSystem.Common.EntityValidationConstants.Movie;

    public class Movie
    {
        public Movie()
        {
            Id = Guid.NewGuid();
            MovieGenres = new HashSet<MovieGenre>();
            Reviews = new HashSet<Review>();
            Showtimes = new HashSet<Showtime>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [MaxLength(PosterImageUrlMaxLength)]
        public string? PosterImageUrl { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Showtime> Showtimes { get; set; }
    }
}
