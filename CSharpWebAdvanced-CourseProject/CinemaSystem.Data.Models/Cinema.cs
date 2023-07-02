using System.ComponentModel.DataAnnotations;
using static CinemaSystem.Common.EntityValidationConstants.Cinema;

namespace CinemaSystem.Data.Models
{
    public class Cinema
    {
        public Cinema()
        {
            Showtimes = new HashSet<Showtime>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string? ImageUrl { get; set; }

        public virtual ICollection<Showtime> Showtimes { get; set; }
    }
}
