namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Web.ViewModels.Cinema;
    using CinemaSystem.Web.ViewModels.Home;
    using System;
    using System.Collections.Generic;

    public interface ICinemaService
    {
        Task<IEnumerable<CinemaViewModel>> GetAllCinemasAsync();
        Task<IEnumerable<DateTime>> GetCinemaAvailableDatesAsync(int cinemaId);
    }
}
