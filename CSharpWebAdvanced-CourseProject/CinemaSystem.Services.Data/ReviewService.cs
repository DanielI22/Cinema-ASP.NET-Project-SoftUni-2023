namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;
    using Review = CinemaSystem.Data.Models.Review;

    public class ReviewService : IReviewService
    {
        private readonly CinemaSystemDbContext dbContext;

        public ReviewService(CinemaSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteReviewAsync(Guid reviewId)
        {
            Review review = await GetReviewByIdAsync(reviewId);
            dbContext.Reviews.Remove(review);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Review> GetReviewByIdAsync(Guid reviewId)
        {
            Review? review = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);
            if(review == null)
            {
                throw new InvalidOperationException("Review not found");
            }
            return review;
        }

        public async Task PostReviewAsync(Guid movieId, Guid userId, string reviewText)
        {
            Review review = new Review
            {
                MovieId = movieId,
                UserId = userId,
                ReviewText = reviewText,
                CreatedOn = DateTime.Now
            };

            await dbContext.Reviews.AddAsync(review);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsReviewCreatorAsync(Guid reviewId, Guid userId)
        {
            Review review = await GetReviewByIdAsync(reviewId);
            return review.UserId == userId;
        }
    }
}
