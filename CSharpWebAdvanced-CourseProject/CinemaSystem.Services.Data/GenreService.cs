namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Genre;
    using CinemaSystem.Web.ViewModels.Movie;
    using Microsoft.EntityFrameworkCore;

    public class GenreService : IGenreService
    {
        private readonly CinemaSystemDbContext dbContext;

        public GenreService(CinemaSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
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
                  .Select(g => new GenreViewModel
                  {
                      Id = g.Id,
                      Name = g.Name,
                  }).ToListAsync();
        }
    }
}
