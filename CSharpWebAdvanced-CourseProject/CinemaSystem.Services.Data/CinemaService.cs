namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Cinema;
    using CinemaSystem.Web.ViewModels.Home;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CinemaService : ICinemaService
    {
        private readonly CinemaSystemDbContext dbContext;

        public CinemaService(CinemaSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CinemaViewModel>> GetAllCinemasAsync()
        {
            return await dbContext.Cinemas
               .Select (c => new CinemaViewModel
               {
                   Id = c.Id,
                   Name = c.Name,
                   Address = c.Address,
                   ImageUrl = c.ImageUrl,
               })
               .ToListAsync();
        }

        public async Task<IEnumerable<DateTime>> GetCinemaAvailableDatesAsync(int cinemaId)
        {
            List<DateTime> availableDates = await dbContext.Cinemas
                .Where(c => c.Id == cinemaId)
                .SelectMany(c => c.Showtimes.Select(s => s.StartTime.Date))
                .Distinct()
                .ToListAsync();

            return availableDates;
        }
    }
}
