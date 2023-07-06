namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using System;
    using System.Threading.Tasks;

    public class ReviewService : IReviewService
    {
        private readonly CinemaSystemDbContext dbContext;

        public ReviewService(CinemaSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
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
    }
}
