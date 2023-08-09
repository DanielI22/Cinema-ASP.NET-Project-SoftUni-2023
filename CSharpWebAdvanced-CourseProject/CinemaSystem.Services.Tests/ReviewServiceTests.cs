namespace CinemaSystem.Services.Tests
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data;
    using CinemaSystem.Web.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;

    [TestFixture]
    public class ReviewServiceTests
    {
        private DbContextOptions<CinemaSystemDbContext> dbContextOptions;
        private CinemaSystemDbContext dbContext;
        private ReviewService reviewService;

        [SetUp]
        public void Setup()
        {
            dbContextOptions = new DbContextOptionsBuilder<CinemaSystemDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new CinemaSystemDbContext(dbContextOptions);

            var loggerMock = new Mock<ILogger<ReviewService>>();

            reviewService = new ReviewService(dbContext, loggerMock.Object);
        }

        [Test]
        public async Task DeleteReviewAsync_ShouldRemoveReview()
        {
            // Arrange
            var review = new Review
            {
                MovieId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                ReviewText = "This is a review.",
                CreatedOn = DateTime.Now
            };
            dbContext.Reviews.Add(review);
            dbContext.SaveChanges();

            // Act
            await reviewService.DeleteReviewAsync(review.Id.ToString());

            // Assert
            var deletedReview = dbContext.Reviews.Find(review.Id);
            Assert.IsNull(deletedReview);
        }

        [Test]
        public async Task GetReviewByIdAsync_ShouldReturnReview()
        {
            // Arrange
            var review = new Review
            {
                MovieId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                ReviewText = "This is a review.",
                CreatedOn = DateTime.Now
            };
            dbContext.Reviews.Add(review);
            dbContext.SaveChanges();

            // Act
            var retrievedReview = await reviewService.GetReviewByIdAsync(review.Id.ToString());

            // Assert
            Assert.IsNotNull(retrievedReview);
            Assert.That(retrievedReview.Id, Is.EqualTo(review.Id));
        }

        [Test]
        public async Task PostReviewAsync_ShouldAddReviewWithSanitizedText()
        {
            // Arrange
            var movieId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            var reviewText = "<script>alert('XSS');</script>This is a review.";

            // Act
            await reviewService.PostReviewAsync(movieId, userId, reviewText);

            // Assert
            var addedReview = dbContext.Reviews.FirstOrDefault();
            Assert.IsNotNull(addedReview);
            Assert.That(addedReview.ReviewText, Is.EqualTo("This is a review."));
        }

        [Test]
        public async Task IsReviewCreatorAsync_ShouldReturnTrueForReviewCreatedByUser()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var review = new Review
            {
                MovieId = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                ReviewText = "This is a review.",
                CreatedOn = DateTime.Now
            };
            dbContext.Reviews.Add(review);
            dbContext.SaveChanges();

            // Act
            var isCreator = await reviewService.IsReviewCreatorAsync(review.Id.ToString(), userId);

            // Assert
            Assert.IsTrue(isCreator);
        }

        [Test]
        public async Task IsReviewCreatorAsync_ShouldReturnFalseForReviewCreatedByDifferentUser()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var review = new Review
            {
                MovieId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                ReviewText = "This is a review.",
                CreatedOn = DateTime.Now
            };
            dbContext.Reviews.Add(review);
            dbContext.SaveChanges();

            // Act
            var isCreator = await reviewService.IsReviewCreatorAsync(review.Id.ToString(), userId);

            // Assert
            Assert.IsFalse(isCreator);
        }

        [Test]
        public async Task GetMovieReviewsPerPageAsync_ShouldReturnPaginatedMovieReviews()
        {
            // Arrange
            var movieId = Guid.NewGuid().ToString();
            var review1 = new Review
            {
                MovieId = Guid.Parse(movieId),
                UserId = Guid.NewGuid(),
                ReviewText = "Review 1",
                CreatedOn = DateTime.UtcNow.AddMinutes(1)
            };
            var review2 = new Review
            {
                MovieId = Guid.Parse(movieId),
                UserId = Guid.NewGuid(),
                ReviewText = "Review 2",
                CreatedOn = DateTime.UtcNow.AddMinutes(2)
            };
            dbContext.Reviews.AddRange(review1, review2);
            dbContext.SaveChanges();

            // Act
            var paginatedReviews = await reviewService.GetMovieReviewsPerPageAsync(movieId, 1, 1);

            // Assert
            Assert.That(paginatedReviews.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetTotalMovieReviewsCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var movieId = Guid.NewGuid().ToString();
            var review1 = new Review
            {
                MovieId = Guid.Parse(movieId),
                UserId = Guid.NewGuid(),
                ReviewText = "Review 1",
                CreatedOn = DateTime.UtcNow.AddMinutes(1)
            };
            var review2 = new Review
            {
                MovieId = Guid.Parse(movieId),
                UserId = Guid.NewGuid(),
                ReviewText = "Review 2",
                CreatedOn = DateTime.UtcNow.AddMinutes(2)
            };
            dbContext.Reviews.AddRange(review1, review2);
            dbContext.SaveChanges();

            // Act
            var totalCount = await reviewService.GetTotalMovieReviewsCount(movieId);

            // Assert
            Assert.That(totalCount, Is.EqualTo(2));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
