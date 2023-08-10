namespace CinemaSystem.Services.Tests.IntegrationTests
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Areas.Admin.Controllers;
    using CinemaSystem.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using static CinemaSystem.Common.GeneralApplicationConstants;


    [TestFixture]
    public class MovieAdminControllerTests
    {
        private Mock<IMovieService> movieServiceMock;
        private Mock<IGenreService> genreServiceMock;
        private MovieController movieController;

        [SetUp]
        public void Setup()
        {
            movieServiceMock = new Mock<IMovieService>();
            genreServiceMock = new Mock<IGenreService>();
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            movieController = new MovieController(movieServiceMock.Object, genreServiceMock.Object)
            {
                TempData = tempData
            };
        }

        [Test]
        public async Task Index_ReturnsViewWithModel()
        {
            // Arrange
            List<MovieCardViewModel> movies = new List<MovieCardViewModel>
            {
                new MovieCardViewModel { Id = Guid.NewGuid(), Title = "Movie 1" },
                new MovieCardViewModel { Id = Guid.NewGuid(), Title = "Movie 2" }
            };
            movieServiceMock.Setup(service => service.GetAllMoviesCardAsync())
                .ReturnsAsync(movies);

            // Act
            var result = await movieController.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as List<MovieCardViewModel>;
            Assert.IsNotNull(model);
            Assert.That(model.Count, Is.EqualTo(movies.Count));
        }

        [Test]
        public async Task Index_WithServiceException_RedirectsToAdminDashboardWithError()
        {
            // Arrange
            movieServiceMock.Setup(service => service.GetAllMoviesCardAsync())
                .Throws(new Exception("Sample exception"));

            // Act
            var result = await movieController.Index() as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Dashboard"));
            Assert.That(result.ControllerName, Is.EqualTo("Admin"));
            Assert.That(movieController.TempData["ErrorMessage"], Is.EqualTo(GeneralError));
        }

        [Test]
        public async Task Add_GET_ReturnsViewWithModel()
        {
            // Arrange
            List<GenreViewModel> genres = new List<GenreViewModel>
            {
                new GenreViewModel { Id = 1, Name = "Genre 1" },
                new GenreViewModel { Id = 2, Name = "Genre 2" }
            };
            genreServiceMock.Setup(service => service.GetGenresAsync())
                .ReturnsAsync(genres);

            // Act
            var result = await movieController.Add() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as MovieAddEditViewModel;
            Assert.IsNotNull(model);
            Assert.That(model.Genres.Count, Is.EqualTo(genres.Count()));
        }

        [Test]
        public async Task Add_GET_WithServiceException_RedirectsToAdminDashboardWithError()
        {
            // Arrange
            genreServiceMock.Setup(service => service.GetGenresAsync())
                .Throws(new Exception("Sample exception"));

            // Act
            var result = await movieController.Add() as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Dashboard"));
            Assert.That(result.ControllerName, Is.EqualTo("Admin"));
            Assert.That(movieController.TempData["ErrorMessage"], Is.EqualTo(GeneralError));
        }

        [Test]
        public async Task Add_POST_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            MovieAddEditViewModel movie = new MovieAddEditViewModel();
            movieServiceMock.Setup(service => service.AddMovieAsync(movie))
                .Returns(Task.CompletedTask);

            // Act
            var result = await movieController.Add(movie) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task Add_POST_WithInvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            MovieAddEditViewModel movie = new MovieAddEditViewModel();
            movieController.ModelState.AddModelError("Title", "Title is required.");

            // Act
            var result = await movieController.Add(movie) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as MovieAddEditViewModel;
            Assert.IsNotNull(model);
        }

        [Test]
        public async Task AddTitle_WithValidInput_RedirectsToIndex()
        {
            // Arrange
            string title = "Sample Movie Title";
            int year = 2023;
            movieServiceMock.Setup(service => service.AddMovieApiTitleAsync(title, year))
                .Returns(Task.CompletedTask);

            // Act
            var result = await movieController.AddTitle(title, year) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task AddTitle_WithInvalidInput_RedirectsToAddWithError()
        {
            // Arrange
            string title = null!;

            // Act
            var result = await movieController.AddTitle(title, 2023) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Add"));
            Assert.IsNull(result.ControllerName);
        }

        [Test]
        public async Task AddId_WithValidInput_RedirectsToIndex()
        {
            // Arrange
            string imdbTag = "tt1234567";
            movieServiceMock.Setup(service => service.AddMovieApiIdAsync(imdbTag))
                .Returns(Task.CompletedTask);

            // Act
            var result = await movieController.AddId(imdbTag) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task AddId_WithInvalidInput_RedirectsToAddWithError()
        {
            // Arrange
            string imdbTag = null!;

            // Act
            var result = await movieController.AddId(imdbTag) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Add"));
            Assert.IsNull(result.ControllerName);
        }

        [Test]
        public async Task Edit_GET_WithValidId_ReturnsViewWithModel()
        {
            // Arrange
            string movieId = "sampleMovieId";
            MovieAddEditViewModel movieModel = new MovieAddEditViewModel();
            movieServiceMock.Setup(service => service.GetEditMovieModelAsync(movieId))
                .ReturnsAsync(movieModel);

            // Act
            var result = await movieController.Edit(movieId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as MovieAddEditViewModel;
            Assert.IsNotNull(model);
            Assert.That(model, Is.EqualTo(movieModel));
        }

        [Test]
        public async Task Edit_GET_WithInvalidId_RedirectsToIndexWithError()
        {
            // Arrange
            string movieId = null!;

            // Act
            var result = await movieController.Edit(movieId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.IsNull(result.ControllerName);
            Assert.That(movieController.TempData["ErrorMessage"], Is.EqualTo(GeneralError));
        }

        [Test]
        public async Task Edit_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            string movieId = "sampleMovieId";
            MovieAddEditViewModel movieModel = new MovieAddEditViewModel();
            movieServiceMock.Setup(service => service.EditMovieAsync(movieId, movieModel))
                .Returns(Task.CompletedTask);

            // Act
            var result = await movieController.Edit(movieId, movieModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task Edit_WithInvalidId_RedirectsToIndexWithError()
        {
            // Arrange
            string movieId = null!;
            MovieAddEditViewModel movieModel = new MovieAddEditViewModel();

            // Act
            var result = await movieController.Edit(movieId, movieModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.IsNull(result.ControllerName);
            Assert.That(movieController.TempData["ErrorMessage"], Is.EqualTo(GeneralError));
        }

        [Test]
        public async Task Edit_WithInvalidModel_ReturnsViewWithError()
        {
            // Arrange
            string movieId = "sampleMovieId";
            MovieAddEditViewModel movieModel = new MovieAddEditViewModel();
            movieController.ModelState.AddModelError("PropertyName", "Error Message");

            // Act
            var result = await movieController.Edit(movieId, movieModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Model, Is.EqualTo(movieModel));
        }

        [Test]
        public async Task Delete_WithValidId_RedirectsToIndex()
        {
            // Arrange
            string movieId = "sampleMovieId";
            movieServiceMock.Setup(service => service.DeleteMovieAsync(movieId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await movieController.Delete(movieId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task Delete_WithInvalidId_RedirectsToIndexWithError()
        {
            // Arrange
            string movieId = null!;

            // Act
            var result = await movieController.Delete(movieId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.IsNull(result.ControllerName);
            Assert.That(movieController.TempData["ErrorMessage"], Is.EqualTo(GeneralError));
        }

    }
}
