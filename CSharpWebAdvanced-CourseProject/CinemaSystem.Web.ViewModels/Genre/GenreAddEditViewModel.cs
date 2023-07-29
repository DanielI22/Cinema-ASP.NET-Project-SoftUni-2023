namespace CinemaSystem.Web.ViewModels.Genre
{
    using System.ComponentModel.DataAnnotations;
    using static CinemaSystem.Common.EntityValidationConstants.Genre;


    public class GenreAddEditViewModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
