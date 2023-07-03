namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Home;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MovieService : IMovieService
    {
        private readonly CinemaSystemDbContext dbContext;

        public MovieService(CinemaSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<MovieCardViewModel>> GetAllMoviesCardAsync()
        {
            return await dbContext.Movies
                .Select(x => new MovieCardViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    PosterUrl = x.PosterImageUrl
                }).ToListAsync();
        }
    }
}
