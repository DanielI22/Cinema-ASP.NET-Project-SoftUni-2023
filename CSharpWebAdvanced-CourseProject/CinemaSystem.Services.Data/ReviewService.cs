namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Review;
    using Ganss.Xss;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;
    using Review = CinemaSystem.Data.Models.Review;

    public class ReviewService : IReviewService
    {
        private readonly CinemaSystemDbContext dbContext;
        private readonly ILogger<ReviewService> logger;


        public ReviewService(CinemaSystemDbContext dbContext, ILogger<ReviewService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task DeleteReviewAsync(string reviewId)
        {
            Review review = await GetReviewByIdAsync(reviewId);
            dbContext.Reviews.Remove(review);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Review> GetReviewByIdAsync(string reviewId)
        {
            Review? review = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Id.ToString() == reviewId);
            if (review == null)
            {
                logger.LogError("Review not found!");
                throw new InvalidOperationException("Review not found!");
            }
            return review;
        }

        public async Task PostReviewAsync(string movieId, string userId, string reviewText)
        {
            var sanitizer = new HtmlSanitizer();
            reviewText = sanitizer.Sanitize(reviewText);
            Review review = new Review
            {
                MovieId = Guid.Parse(movieId),
                UserId = Guid.Parse(userId),
                ReviewText = reviewText,
                CreatedOn = DateTime.Now
            };

            await dbContext.Reviews.AddAsync(review);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsReviewCreatorAsync(string reviewId, string userId)
        {
            Review review = await GetReviewByIdAsync(reviewId);
            return review.UserId.ToString() == userId;
        }

        public async Task<IEnumerable<ReviewViewModel>> GetReviewsDescendingAsync()
        {
            return await dbContext.Reviews
                .OrderByDescending(review => review.CreatedOn)
                .Select(r => new ReviewViewModel
                {
                    ReviewId = r.Id,
                    ReviewAuthor = r.User.UserName,
                    ReviewText = r.ReviewText
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewViewModel>> GetMovieReviewsPerPageAsync(string movieId, int pageNumber, int pageSize)
        {
            IEnumerable<Review> reviews = await dbContext.Reviews
                .Include(r => r.User)
                .Where(r => r.MovieId.ToString() == movieId)
                .OrderByDescending(review => review.CreatedOn)
                .ToListAsync();

            IEnumerable<Review> paginatedReviews = reviews
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            IEnumerable<ReviewViewModel> paginatedReviewViewModels = paginatedReviews.Select(pr => new ReviewViewModel
            {
                ReviewId = pr.Id,
                CreatorId = pr.UserId,
                ReviewText = pr.ReviewText,
                ReviewAuthor = pr.User.UserName,
            });

            return paginatedReviewViewModels;
        }

        public async Task<int> GetTotalMovieReviewsCount(string movieId)
        {
            return await dbContext.Reviews.Where(r => r.MovieId.ToString() == movieId).CountAsync();
        }
    }
}
