namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels;
    using CinemaSystem.Web.ViewModels.Home;
    using CinemaSystem.Web.ViewModels.Movie;
    using CinemaSystem.Web.ViewModels.Review;
    using Microsoft.EntityFrameworkCore;
    using System;
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
                .Select(m => new MovieCardViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterImageUrl
                }).ToListAsync();
        }

        public async Task<MovieDetailsViewModel?> GetNewMovieDetailsModelAsync(Guid id)
        {
            return await dbContext.Movies
            .Where(m => m.Id == id)
            .Include(m => m.Reviews)
            .Include(m => m.MovieGenres)
            .ThenInclude(mg => mg.Genre)
            .Select(m => new MovieDetailsViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ReleaseYear = m.ReleaseYear,
                Genres = m.MovieGenres.Select(mg => mg.Genre.Name),
                Reviews = m.Reviews.OrderByDescending(r => r.CreatedOn)
                .Select(r => new ReviewViewModel
                {
                    ReviewId = r.Id,
                    CreatorId = r.UserId,
                    ReviewAuthor = r.User.UserName,
                    ReviewText = r.ReviewText
                }),
                PosterUrl = m.PosterImageUrl
            }).FirstOrDefaultAsync();
        }
    }
}
