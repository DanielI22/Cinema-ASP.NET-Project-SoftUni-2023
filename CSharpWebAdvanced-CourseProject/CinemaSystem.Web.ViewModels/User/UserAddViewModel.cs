namespace CinemaSystem.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class UserAddViewModel
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public bool IsAdmin { get; set; } = false;
    }
}
