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
        private readonly IReviewService reviewService;

        public MovieService(CinemaSystemDbContext dbContext, IReviewService reviewService)
        {
            this.dbContext = dbContext;
            this.reviewService = reviewService;
        }

        public async Task<IEnumerable<MovieCardViewModel>> FilterMoviesAsync(string? searchName, int selectedGenreId)
        {
            IEnumerable<MovieCardViewModel> allMovies = await GetAllMoviesCardAsync();

            // Filter by searchName
            if (!string.IsNullOrEmpty(searchName))
            {
                allMovies = allMovies.Where(m => m.Title.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Filter by selectedGenre
            if (selectedGenreId != 0)
            {
                allMovies = allMovies
                    .Where(m => m.Genres.Any(g => g.Id == selectedGenreId))
                    .ToList();
            }

            return allMovies;
        }

        public async Task<IEnumerable<MovieCardViewModel>> GetAllMoviesCardAsync()
        {
            return await dbContext.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Select(m => new MovieCardViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterImageUrl,
                    Genres = m.MovieGenres.Select(mg => new GenreViewModel
                    {
                        Id = mg.GenreId,
                        Name = mg.Genre.Name
                    })
                }).ToListAsync();
        }

        public async Task<MovieDetailsViewModel?> GetMovieDetailsModelAsync(Guid movieId, int pageNumber, int pageSize)
        {
            var reviews = await reviewService.GetMovieReviewsPerPageAsync(movieId, pageNumber, pageSize);
            var totalReviews = await reviewService.GetTotalMovieReviwsCount(movieId);

             MovieDetailsViewModel? movieModel = await dbContext.Movies
            .Where(m => m.Id == movieId)
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
                PosterUrl = m.PosterImageUrl,
                TotalReviews = totalReviews,
                CurrentPage = pageNumber,
                PageSize = pageSize
            }).FirstOrDefaultAsync();

            if(movieModel == null)
            {
                throw new InvalidOperationException("Movie not found!");
            }

            movieModel.Reviews = reviews;
            movieModel.TotalReviews = totalReviews;
            movieModel.CurrentPage = pageNumber;
            movieModel.PageSize = pageSize;

            return movieModel;
        }
    }
}
