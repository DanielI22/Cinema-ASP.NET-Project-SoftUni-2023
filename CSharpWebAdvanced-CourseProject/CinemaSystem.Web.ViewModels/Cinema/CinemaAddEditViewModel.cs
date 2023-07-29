namespace CinemaSystem.Web.ViewModels.Cinema
{
    using System.ComponentModel.DataAnnotations;
    using static CinemaSystem.Common.EntityValidationConstants.Cinema;


    public class CinemaAddEditViewModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(AddressMinLength)]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;


        [MaxLength(ImageUrlMaxLength)]
        [Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }
    }
}
