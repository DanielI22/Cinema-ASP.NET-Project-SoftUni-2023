namespace CinemaSystem.Services.Tests
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Services.Data;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Ticket;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;

        [TestFixture]
        public class TicketServiceTests
        {
            private CinemaSystemDbContext dbContext;
            private ITicketService ticketService;
            private Mock<UserManager<ApplicationUser>> userManagerMock;

        [SetUp]
            public void Setup()
            {
                var options = new DbContextOptionsBuilder<CinemaSystemDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;

                dbContext = new CinemaSystemDbContext(options);
                var showtimeServiceMock = new Mock<IShowtimeService>();
                userManagerMock = new Mock<UserManager<ApplicationUser>>(
               new Mock<IUserStore<ApplicationUser>>().Object,
               null, null, null, null, null, null, null, null);

            var loggerMock = new Mock<ILogger<TicketService>>();

                ticketService = new TicketService(dbContext, showtimeServiceMock.Object, userManagerMock.Object, loggerMock.Object);
            }

            [TearDown]
            public void TearDown()
            {
                dbContext.Database.EnsureDeleted();
            }

            [Test]
            public async Task AddTicketAsync_ShouldAddTicketToDatabase()
            {
                // Arrange
                var model = new TicketAddEditViewModel
                {
                    ShowtimeId = 1,
                    UserId = Guid.NewGuid().ToString(),
                    TicketPrice = 10.0m,
                    SeatNumber = 1
                };

                // Act
                await ticketService.AddTicketAsync(model);

                // Assert
                var ticket = await dbContext.Tickets.FirstOrDefaultAsync();
                Assert.NotNull(ticket);
                Assert.That(dbContext.Tickets.Count(), Is.EqualTo(1));
            }

            [Test]
            public async Task DeleteTicketAsync_ShouldDeactivateTicket()
            {
                // Arrange
                var ticket = new Ticket
                {
                    ShowtimeId = 1,
                    UserId = Guid.NewGuid(),
                    Price = 10.0m,
                    SeatNumber = "1",
                    isActive = true
                };
                dbContext.Tickets.Add(ticket);
                dbContext.SaveChanges();

                // Act
                await ticketService.DeleteTicketAsync(ticket.Id.ToString());

                // Assert
                Assert.IsFalse(ticket.isActive);
            }

            [Test]
            public async Task EditTicketAsync_ShouldEditTicketInDatabase()
            {
                // Arrange
                var ticket = new Ticket
                {
                    ShowtimeId = 1,
                    UserId = Guid.NewGuid(),
                    Price = 10.0m,
                    SeatNumber = "1",
                    isActive = true
                };
                dbContext.Tickets.Add(ticket);
                dbContext.SaveChanges();
                var model = new TicketAddEditViewModel
                {
                    ShowtimeId = 1,
                    UserId = Guid.NewGuid().ToString(),
                    TicketPrice = 15.0m,
                    SeatNumber = 2
                };

                // Act
                await ticketService.EditTicketAsync(ticket.Id.ToString(), model);

                // Assert
                var editedTicket = await dbContext.Tickets.FindAsync(ticket.Id);
                Assert.That(editedTicket?.Price, Is.EqualTo(15.0));
                Assert.That(editedTicket.SeatNumber, Is.EqualTo("2"));
            }

        [Test]
        public void GetSelectedSeatsAsync_ShouldReturnSeatsForValidShowtimeId()
        {
            // Arrange
            var showtimeId = 1;
            var ticket = new Ticket
            {
                ShowtimeId = showtimeId,
                UserId = Guid.NewGuid(),
                Price = 10.0m,
                SeatNumber = "1",
                isActive = true
            };
            dbContext.Tickets.Add(ticket);
            dbContext.SaveChanges();

            // Act
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                var selectedSeats = await ticketService.GetSelectedSeatsAsync(showtimeId.ToString());
            });

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Showtime could not be found in the database!"));

        }

        [Test]
            public async Task GetTicketsAsync_ShouldReturnTickets()
            {
                // Arrange
                var ticket = new Ticket
                {
                    ShowtimeId = 1,
                    UserId = Guid.NewGuid(),
                    Price = 10.0m,
                    SeatNumber = "1",
                    isActive = true
                };
                dbContext.Tickets.Add(ticket);
                dbContext.SaveChanges();

                // Act
                var tickets = await ticketService.GetTicketsAsync();

                // Assert
                Assert.NotNull(tickets);
                Assert.That(tickets.Count(), Is.EqualTo(0));
            }

            [Test]
            public async Task ReserveTicketsAsync_ShouldReserveTicketsForValidShowtime()
            {
                // Arrange
                var showtimeId = 1;
                var userId = Guid.NewGuid().ToString();
                var selectedSeats = new List<int> { 1, 2, 3 };
                var showtime = new Showtime
                {
                    Id = showtimeId,
                    Tickets = new List<Ticket>(),
                    TicketPrice = 10.0m
                };
                dbContext.Showtimes.Add(showtime);
                dbContext.SaveChanges();

                // Act
                await ticketService.ReserveTicketsAsync(showtimeId.ToString(), userId, selectedSeats);

                // Assert
                Assert.That(showtime.Tickets.Count(), Is.EqualTo(3));
            }

        [Test]
        public void DeleteTicketAsync_InvalidId_ThrowsInvalidOperationException()
        {
            // Arrange
            var ticketId = "invalid_id";

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await ticketService.DeleteTicketAsync(ticketId);
            });

            Assert.That(ex.Message, Is.EqualTo("Ticket could not be found in the database!"));
        }

    }
}
