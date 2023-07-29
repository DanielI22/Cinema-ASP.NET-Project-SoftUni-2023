namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Cinema;
    using CinemaSystem.Web.ViewModels.Home;
    using System;
    using System.Collections.Generic;

    public interface ICinemaService
    {
        Task AddCinemaAsync(CinemaAddEditViewModel cinema);
        Task DeleteCinemaAsync(string id);
        Task EditCinemaAsync(string id, CinemaAddEditViewModel cinema);
        Task<IEnumerable<CinemaViewModel>> GetAllCinemasAsync();
        Task<IEnumerable<DateTime>> GetCinemaAvailableDatesAsync(int cinemaId);
        Task<CinemaAddEditViewModel?> GetEditCinemaModelAsync(string id);
    }
}
