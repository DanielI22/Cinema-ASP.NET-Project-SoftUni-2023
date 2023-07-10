namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Movie;
    using Microsoft.EntityFrameworkCore;

    public class GenreService : IGenreService
    {
        private readonly CinemaSystemDbContext dbContext;

        public GenreService(CinemaSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<GenreViewModel>> GetGenresAsync()
        {
            return await dbContext.Genres
                  .Select(g => new GenreViewModel
                  {
                      Id = g.Id,
                      Name = g.Name,
                  }).ToListAsync();
        }
    }
}
