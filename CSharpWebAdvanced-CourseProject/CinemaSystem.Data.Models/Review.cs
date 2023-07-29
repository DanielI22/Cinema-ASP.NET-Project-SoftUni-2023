namespace CinemaSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static CinemaSystem.Common.EntityValidationConstants.Review;

    public class Review
    {
        public Review()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid MovieId { get; set; }

        [ForeignKey(nameof(MovieId))]
        public virtual Movie Movie { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [MaxLength(TextMaxLength)]
        public string ReviewText { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
