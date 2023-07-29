namespace CinemaSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static CinemaSystem.Common.EntityValidationConstants.Genre;

    public class Genre
    {
        public Genre()
        {
            MovieGenres = new HashSet<MovieGenre>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }

        public bool isActive { get; set; } = true;

    }
}
