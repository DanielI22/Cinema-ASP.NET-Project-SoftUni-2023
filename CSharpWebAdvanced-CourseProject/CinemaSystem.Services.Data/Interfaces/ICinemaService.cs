namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Home;

    public interface ICinemaService
    {
        Task<ICollection<CinemaIndexViewModel>> GetAllCinemasAsync();
    }
}
