namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Cinema;
    using CinemaSystem.Web.ViewModels.Home;

    public interface ICinemaService
    {
        Task<IEnumerable<CinemaViewModel>> GetAllCinemasAsync();
    }
}
