namespace CinemaSystem.Services.Tests.IntegrationTests
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Ticket;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using System.Security.Claims;
    using static CinemaSystem.Common.GeneralApplicationConstants;
    using TicketController = Web.Controllers.TicketController;


    [TestFixture]
    public class TicketControllerTests
    {
        private Mock<ITicketService> ticketServiceMock;
        private TicketController ticketController;

        [SetUp]
        public void Setup()
        {
            ticketServiceMock = new Mock<ITicketService>();
            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "sampleuser"),
                new Claim(ClaimTypes.NameIdentifier, "1")
            }));
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            ticketController = new TicketController(ticketServiceMock.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };
        }

        [Test]
        public async Task GetSeatsAsync_ReturnsJsonResult()
        {
            // Arrange
            string showtimeId = "sampleShowtimeId";
            List<int> seats = new List<int>
            {
                1, 2
            };
            ticketServiceMock.Setup(service => service.GetSelectedSeatsAsync(showtimeId))
                .ReturnsAsync(seats);

            // Act
            var result = await ticketController.GetSeatsAsync(showtimeId) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var resultValue = result.Value as List<int>;
            Assert.IsNotNull(resultValue);
            Assert.That(resultValue.Count, Is.EqualTo(seats.Count));
        }

        [Test]
        public async Task Reserve_GET_WithValidShowtimeId_ReturnsViewWithModel()
        {
            // Arrange
            string showtimeId = "sampleShowtimeId";
            List<int> seats = new List<int>
            {
                1,2
            };
            ticketServiceMock.Setup(service => service.GetSelectedSeatsAsync(showtimeId))
                .ReturnsAsync(seats);

            // Act
            var result = await ticketController.Reserve(showtimeId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as TicketSelectionViewModel;
            Assert.IsNotNull(model);
            Assert.That(model.ShowtimeId, Is.EqualTo(showtimeId));
            Assert.That(model.ReservedSeats.Count(), Is.EqualTo(seats.Count));
        }

        [Test]
        public async Task Reserve_GET_WithInvalidShowtimeId_RedirectsToHomeWithError()
        {
            // Arrange

            // Act
            var result = await ticketController.Reserve(null!) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
            Assert.That(ticketController.TempData["ErrorMessage"], Is.EqualTo(GeneralError));
        }

        [Test]
        public async Task Reserve_GET_WithServiceException_RedirectsToHomeWithError()
        {
            // Arrange
            string showtimeId = "sampleShowtimeId";
            ticketServiceMock.Setup(service => service.GetSelectedSeatsAsync(showtimeId))
                .Throws(new Exception("Sample exception"));

            // Act
            var result = await ticketController.Reserve(showtimeId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
            Assert.That(ticketController.TempData["ErrorMessage"], Is.EqualTo(GeneralError));
        }

        [Test]
        public async Task Reserve_POST_WithValidData_ReservesTicketsAndRedirectsToHomeWithSuccess()
        {
            // Arrange
            string showtimeId = "sampleShowtimeId";
            string selectedSeats = "1,2";
            string userId = "sampleUserId";
            List<int> selectedSeatsNumbers = new List<int> { 1, 2 };
            ticketServiceMock.Setup(service => service.ReserveTicketsAsync(showtimeId, userId, selectedSeatsNumbers))
                .Returns(Task.CompletedTask);

            // Act
            var result = await ticketController.Reserve(showtimeId, selectedSeats) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
            Assert.That(ticketController.TempData["SuccessMessage"], Is.EqualTo("You reservation is completed. We are expecting you!"));
        }

        [Test]
        public async Task Reserve_POST_WithInvalidData_RedirectsToHomeWithError()
        {
            // Arrange

            // Act
            var result = await ticketController.Reserve(null!, null!) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
            Assert.That(ticketController.TempData["ErrorMessage"], Is.EqualTo(GeneralError));
        }

        [Test]
        public async Task Reserve_POST_WithServiceException_RedirectsToHomeWithError()
        {
            // Arrange
            string showtimeId = "sampleShowtimeId";
            string selectedSeats = "1,2";
            string userId = "sampleUserId";
            List<int> selectedSeatsNumbers = new List<int> { 1, 2 };
            ticketServiceMock.Setup(service => service.ReserveTicketsAsync(showtimeId, userId, selectedSeatsNumbers))
                .Throws(new Exception("Sample exception"));

            // Act
            var result = await ticketController.Reserve(showtimeId, selectedSeats) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
        }
    }
}
