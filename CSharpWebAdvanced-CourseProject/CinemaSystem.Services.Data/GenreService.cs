namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Genre;
    using CinemaSystem.Web.ViewModels.Movie;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class GenreService : IGenreService
    {
        private readonly CinemaSystemDbContext dbContext;
        private readonly ILogger<GenreService> logger;

        public GenreService(CinemaSystemDbContext dbContext, ILogger<GenreService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task AddGenreAsync(GenreAddEditViewModel model)
        {
            Genre genre = new Genre
            {
                Name = model.Name,
            };

            await dbContext.Genres.AddAsync(genre);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteGenreAsync(string id)
        {
            var genre = await dbContext.Genres
           .FirstOrDefaultAsync(c => c.Id.ToString() == id);

            if (genre != null)
            {
                genre.isActive = false;
            }
            else
            {
                string error = "Genre could not be found in the database!";
                logger.LogError(error);
                throw new InvalidOperationException(error);
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task EditGenreAsync(string id, GenreAddEditViewModel model)
        {
            var genre = await dbContext.Genres
            .FirstOrDefaultAsync(g => g.Id.ToString() == id);

            if (genre != null)
            {
                genre.Name = model.Name;
                await dbContext.SaveChangesAsync();
            }
            else
            {
                string error = "Cinema could not be found in the database!";
                logger.LogError(error);
                throw new InvalidOperationException(error);
            }
        }

        public async Task<GenreAddEditViewModel?> GetEditGenreModelAsync(string id)
        {
            return await dbContext.Genres
               .Where(c => c.Id.ToString() == id)
               .Select(c => new GenreAddEditViewModel
               {
                   Name = c.Name
               }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GenreViewModel>> GetGenresAsync()
        {
            return await dbContext.Genres
                  .Where(g => g.isActive)
                  .OrderBy(g => g.Name)
                  .Select(g => new GenreViewModel
                  {
                      Id = g.Id,
                      Name = g.Name,
                  }).ToListAsync();
        }
    }
}
