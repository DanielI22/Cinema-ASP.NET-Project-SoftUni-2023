namespace CinemaSystem.Web.ViewModels.Cinema
{
    public class CinemaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
