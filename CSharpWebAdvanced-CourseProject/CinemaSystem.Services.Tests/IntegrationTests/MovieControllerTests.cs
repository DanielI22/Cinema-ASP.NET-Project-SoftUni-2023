namespace CinemaSystem.Services.Tests.IntegrationTests
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Controllers;
    using CinemaSystem.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestFixture]
    public class MovieControllerTests
    {
        private MovieController movieController;

        [SetUp]
        public void SetUp()
        {
            var movieServiceMock = new Mock<IMovieService>();
            var genreServiceMock = new Mock<IGenreService>();
            var loggerMock = new Mock<ILogger<MovieController>>();

            movieServiceMock.Setup(service => service.GetMovieDetailsModelAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new MovieDetailsViewModel
                {
                });

            movieServiceMock.Setup(service => service.FilterMoviesAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new List<MovieCardViewModel>
                {
                });

            genreServiceMock.Setup(service => service.GetGenresAsync())
                .ReturnsAsync(new List<GenreViewModel>
                {
                });

            movieController = new MovieController(movieServiceMock.Object, genreServiceMock.Object);
        }

        [Test]
        public async Task Details_ValidId_ReturnsViewWithModel()
        {
            // Arrange
            string movieId = "sampleId";

            // Act
            var result = await movieController.Details(movieId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<MovieDetailsViewModel>(result.Model);
        }

        [Test]
        public async Task All_ReturnsViewWithMoviesAndGenres()
        {
            // Arrange
            var moviesViewModel = new MoviesViewModel
            {
                SearchName = "SampleSearch",
                SelectedGenreId = 1
            };

            // Act
            var result = await movieController.All(moviesViewModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<MoviesViewModel>(result.Model);
        }
    }
}
