namespace CinemaSystem.Services.Tests.IntegrationTests
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Controllers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Security.Claims;
    using System.Threading.Tasks;


    [TestFixture]
    public class ReviewControllerTests
    {
        private ReviewController reviewController;
        private Mock<IReviewService> reviewServiceMock;

        [SetUp]
        public void SetUp()
        {
            reviewServiceMock = new Mock<IReviewService>();
            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "sampleuser"),
                new Claim(ClaimTypes.NameIdentifier, "1")
            }));

            reviewController = new ReviewController(reviewServiceMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };
        }

        [Test]
        public async Task Post_ValidReview_RedirectsToMovieDetails()
        {
            // Arrange
            string movieId = "sampleMovieId";
            string review = "Sample review text";

            // Act
            var result = await reviewController.Post(movieId, review) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo(nameof(MovieController.Details)));
            Assert.That(result?.ControllerName, Is.EqualTo("Movie"));
            Assert.That(result?.RouteValues?["id"], Is.EqualTo(movieId));
        }

        [Test]
        public async Task DeleteMy_ValidReviewAndUser_RedirectsToMovieDetails()
        {
            // Arrange
            string reviewId = "sampleReviewId";
            string movieId = "sampleMovieId";

            reviewServiceMock.Setup(service => service.IsReviewCreatorAsync(reviewId, It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await reviewController.DeleteMy(reviewId, movieId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo(nameof(MovieController.Details)));
            Assert.That(result.ControllerName, Is.EqualTo("Movie"));
            Assert.That(result.RouteValues?["id"], Is.EqualTo(movieId));
        }
    }
}
