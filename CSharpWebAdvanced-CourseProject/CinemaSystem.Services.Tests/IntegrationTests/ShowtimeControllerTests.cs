namespace CinemaSystem.Services.Tests.IntegrationTests
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Controllers;
    using CinemaSystem.Web.ViewModels.Movie;
    using CinemaSystem.Web.ViewModels.Showtime;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [TestFixture]
    public class ShowtimeControllerTests
    {
        private ShowtimeController showtimeController;
        private Mock<IShowtimeService> showtimeServiceMock;
        private Mock<ICinemaService> cinemaServiceMock;

        [SetUp]
        public void SetUp()
        {
            showtimeServiceMock = new Mock<IShowtimeService>();
            cinemaServiceMock = new Mock<ICinemaService>();
            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "sampleuser"),
                new Claim(ClaimTypes.NameIdentifier, "1")
            }));
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            showtimeController = new ShowtimeController(showtimeServiceMock.Object, cinemaServiceMock.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };
        }
        [Test]
        public async Task Select_ValidCinemaIdAndNoSelectedDate_ReturnsViewWithViewModel()
        {
            // Arrange
            int cinemaId = 1;
            var availableDates = new List<DateTime> { DateTime.Now.Date, DateTime.Now.Date.AddDays(1) };
            cinemaServiceMock.Setup(service => service.GetCinemaAvailableDatesAsync(cinemaId))
                .ReturnsAsync(availableDates);

            // Act
            var result = await showtimeController.Select(cinemaId, null!) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ShowtimeSelectViewModel>(result.Model);
            var viewModel = result.Model as ShowtimeSelectViewModel;
            Assert.That(viewModel?.CinemaId, Is.EqualTo(cinemaId));
            Assert.That(viewModel.SelectedDate, Is.EqualTo(availableDates.First().ToShortDateString()));
            Assert.That(viewModel.Dates.Count(), Is.EqualTo(availableDates.Count()));
        }

        [Test]
        public async Task Select_ValidCinemaIdAndSelectedDate_ReturnsViewWithViewModel()
        {
            // Arrange
            int cinemaId = 1;
            string selectedDate = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd");
            var availableDates = new List<DateTime> { DateTime.Now.Date, DateTime.Now.Date.AddDays(1) };
            var movieShowtimes = new List<MovieShowtimeViewModel> { new MovieShowtimeViewModel() };
            cinemaServiceMock.Setup(service => service.GetCinemaAvailableDatesAsync(cinemaId))
                .ReturnsAsync(availableDates);
            showtimeServiceMock.Setup(service => service.GetMovieShowtimesForCinemaDateAsync(cinemaId, DateTime.Parse(selectedDate)))
                .ReturnsAsync(movieShowtimes);

            // Act
            var result = await showtimeController.Select(cinemaId, selectedDate) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ShowtimeSelectViewModel>(result.Model);
            var viewModel = result.Model as ShowtimeSelectViewModel;
            Assert.That(viewModel?.CinemaId, Is.EqualTo(cinemaId));
            Assert.That(viewModel.Dates.Count(), Is.EqualTo(availableDates.Count()));
            Assert.That(viewModel.Movies.Count(), Is.EqualTo(movieShowtimes.Count()));
        }

        [Test]
        public async Task Select_InvalidCinemaId_RedirectsToHomeWithError()
        {
            // Arrange
            int invalidCinemaId = 0;

            // Act
            var result = await showtimeController.Select(invalidCinemaId, null!) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
        }

        [Test]
        public async Task Select_ExceptionThrown_RedirectsToHomeWithError()
        {
            // Arrange
            int cinemaId = 1;
            cinemaServiceMock.Setup(service => service.GetCinemaAvailableDatesAsync(cinemaId))
                .Throws(new Exception());

            // Act
            var result = await showtimeController.Select(cinemaId, null!) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
        }
    }
}
