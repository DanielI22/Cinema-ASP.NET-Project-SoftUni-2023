namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Cinema;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CinemaService : ICinemaService
    {
        private readonly CinemaSystemDbContext dbContext;
        private readonly IMemoryCache memoryCache;

        public CinemaService(CinemaSystemDbContext dbContext, IMemoryCache memoryCache)
        {
            this.dbContext = dbContext;
            this.memoryCache = memoryCache;
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
            }
            await dbContext.SaveChangesAsync();
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
        }

        public async Task<IEnumerable<CinemaViewModel>> GetAllCinemasAsync()
        {
            if (!memoryCache.TryGetValue("Cinemas", out List<CinemaViewModel> cinemas))
            {
                cinemas = await dbContext.Cinemas
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

                memoryCache.Set("Cinemas", cinemas, TimeSpan.FromMinutes(1));
            }
            return cinemas;
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
