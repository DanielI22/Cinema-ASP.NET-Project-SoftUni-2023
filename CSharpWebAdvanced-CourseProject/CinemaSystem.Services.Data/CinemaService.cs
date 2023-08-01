namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Cinema;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CinemaService : ICinemaService
    {
        private readonly CinemaSystemDbContext dbContext;
        private readonly ILogger<CinemaService> logger;

        public CinemaService(CinemaSystemDbContext dbContext, ILogger<CinemaService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task AddCinemaAsync(CinemaAddEditViewModel model)
        {
            Cinema cinema = new Cinema
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                Address = model.Address
            };

            await dbContext.Cinemas.AddAsync(cinema);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCinemaAsync(string id)
        {
            var cinema = await dbContext.Cinemas
            .FirstOrDefaultAsync(c => c.Id.ToString() == id);

            if (cinema != null)
            {
                cinema.isActive = false;
                await dbContext.SaveChangesAsync();
            }
            else
            {
                string error = "Cinema could not be found in the database!";
                logger.LogError(error);
                throw new InvalidOperationException(error);
            }
        }

        public async Task EditCinemaAsync(string id, CinemaAddEditViewModel model)
        {
            var cinema = await dbContext.Cinemas
               .FirstOrDefaultAsync(c => c.Id.ToString() == id);

            if (cinema != null)
            {
                cinema.Name = model.Name;
                cinema.Address = model.Address;
                cinema.ImageUrl = model.ImageUrl;
                await dbContext.SaveChangesAsync();
            }
            else
            {
                string error = "Cinema could not be found in the database!";
                logger.LogError(error);
                throw new InvalidOperationException(error);
            }
        }

        public async Task<IEnumerable<CinemaViewModel>> GetAllCinemasAsync()
        {
            return await dbContext.Cinemas
           .Where(c => c.isActive)
           .OrderBy(c => c.Name)
           .Select(c => new CinemaViewModel
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
                .SelectMany(c => c.Showtimes.Where(sh => sh.isActive).Select(s => s.StartTime.Date))
                .Distinct()
                .ToListAsync();

            return availableDates;
        }

        public async Task<CinemaAddEditViewModel?> GetEditCinemaModelAsync(string id)
        {
            return await dbContext.Cinemas
                .Where(c => c.Id.ToString() == id)
                .Select(c => new CinemaAddEditViewModel
                {
                    Name = c.Name,
                    Address = c.Address,
                    ImageUrl = c.ImageUrl,
                }).FirstOrDefaultAsync();
        }
    }
}
