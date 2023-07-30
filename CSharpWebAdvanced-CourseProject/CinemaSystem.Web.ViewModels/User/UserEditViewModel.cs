namespace CinemaSystem.Web.ViewModels.User
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class UserEditViewModel
    {
        [Required]
        public string Username { get; set; } = null!;

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public bool IsAdmin { get; set; } = false;
    }
}
