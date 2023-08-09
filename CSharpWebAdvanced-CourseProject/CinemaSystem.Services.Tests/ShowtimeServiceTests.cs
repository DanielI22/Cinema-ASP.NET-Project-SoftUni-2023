namespace CinemaSystem.Services.Tests
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Services.Data;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Showtime;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using CinemaSystem.Data.Models;

    [TestFixture]
    public class ShowtimeServiceTests
    {
        private CinemaSystemDbContext dbContext;
        private ShowtimeService showtimeService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CinemaSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            dbContext = new CinemaSystemDbContext(options);
            showtimeService = new ShowtimeService(dbContext, new Mock<IMovieService>().Object, new Mock<ILogger<ShowtimeService>>().Object);
        }

        [TearDown]
        public void Cleanup()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Test]
        public async Task AddShowtimeAsync_ValidModel_AddsShowtimeToDatabase()
        {
            // Arrange
            var model = new ShowtimeAddEditViewModel
            {
                CinemaId = 1,
                MovieId = Guid.NewGuid(),
                TicketPrice = 10.0m,
                StartTime = DateTime.Now
            };

            // Act
            await showtimeService.AddShowtimeAsync(model);

            // Assert
            var addedShowtime = await dbContext.Showtimes.FirstOrDefaultAsync();
            Assert.IsNotNull(addedShowtime);
        }

        [Test]
        public async Task DeleteShowtimeAsync_ValidId_SetsShowtimeInactive()
        {
            // Arrange
            var showtime = new Showtime
            {
                Id = 1,
                CinemaId = 1,
                MovieId = Guid.NewGuid(),
                TicketPrice = 10.0m,
                StartTime = DateTime.Now,
                isActive = true
            };
            dbContext.Showtimes.Add(showtime);
            dbContext.SaveChanges();

            // Act
            await showtimeService.DeleteShowtimeAsync(showtime.Id.ToString());

            // Assert
            var deletedShowtime = await dbContext.Showtimes.FirstOrDefaultAsync(s => s.Id == showtime.Id);
            Assert.IsFalse(deletedShowtime?.isActive);
        }

        [Test]
        public async Task EditShowtimeAsync_ValidId_UpdatesShowtimeInDatabase()
        {
            // Arrange
            var showtime = new Showtime
            {
                Id = 1,
                CinemaId = 1,
                MovieId = Guid.NewGuid(),
                TicketPrice = 10.0m,
                StartTime = DateTime.Now,
                isActive = true
            };
            dbContext.Showtimes.Add(showtime);
            dbContext.SaveChanges();

            var model = new ShowtimeAddEditViewModel
            {
                CinemaId = 2,
                MovieId = Guid.NewGuid(),
                TicketPrice = 15.0m,
                StartTime = DateTime.Now.AddHours(2)
            };

            // Act
            await showtimeService.EditShowtimeAsync(showtime.Id.ToString(), model);

            // Assert
            var editedShowtime = await dbContext.Showtimes.FirstOrDefaultAsync(s => s.Id == showtime.Id);
            Assert.That(editedShowtime?.CinemaId, Is.EqualTo(model.CinemaId));
            Assert.That(editedShowtime.MovieId, Is.EqualTo(model.MovieId));
            Assert.That(editedShowtime.TicketPrice, Is.EqualTo(model.TicketPrice));
            Assert.That(editedShowtime.StartTime, Is.EqualTo(model.StartTime));
        }

        [Test]
        public async Task GetEditShowtimeModelAsync_ValidId_ReturnsCorrectViewModel()
        {
            // Arrange
            var cinema = new Cinema
            {
                Id = 1,
                Name = "Test Cinema",
                Address = "Test",
                isActive = true
            };
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = "Test Movie",
                Description = "Test",
                isActive = true
            };
            dbContext.Cinemas.Add(cinema);
            dbContext.Movies.Add(movie);
            dbContext.SaveChanges();

            var showtime = new Showtime
            {
                Id = 1,
                CinemaId = cinema.Id,
                MovieId = movie.Id,
                TicketPrice = 10.0m,
                StartTime = DateTime.Now,
                isActive = true
            };
            dbContext.Showtimes.Add(showtime);
            dbContext.SaveChanges();

            // Act
            var result = await showtimeService.GetEditShowtimeModelAsync(showtime.Id.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.TicketPrice, Is.EqualTo(showtime.TicketPrice));
            Assert.That(result.StartTime, Is.EqualTo(showtime.StartTime));
            Assert.That(result.MovieId, Is.EqualTo(showtime.MovieId));
            Assert.That(result.CinemaId, Is.EqualTo(showtime.CinemaId));
            Assert.IsNotNull(result.Cinemas);
            Assert.IsNotNull(result.Movies);
            // Additional assertions...
        }


        [Test]
        public async Task GetShowtimesAsync_ReturnsCorrectViewModelList()
        {
            // Arrange
            var cinema = new Cinema
            {
                Id = 1,
                Name = "Test Cinema",
                Address = "Test",
                isActive = true
            };
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = "Test Movie",
                Description = "Test",
                isActive = true
            };
            dbContext.Cinemas.Add(cinema);
            dbContext.Movies.Add(movie);
            dbContext.SaveChanges();

            var showtime = new Showtime
            {
                Id = 1,
                CinemaId = cinema.Id,
                MovieId = movie.Id,
                TicketPrice = 10.0m,
                StartTime = DateTime.Now,
                isActive = true
            };
            dbContext.Showtimes.Add(showtime);
            dbContext.SaveChanges();

            // Act
            var result = await showtimeService.GetShowtimesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(1));
            var showtimeViewModel = result.FirstOrDefault();
            Assert.IsNotNull(showtimeViewModel);
            Assert.That(showtimeViewModel.Id, Is.EqualTo(showtime.Id.ToString()));
            Assert.That(showtimeViewModel.TicketPrice, Is.EqualTo(showtime.TicketPrice));
            Assert.That(showtimeViewModel.StartTime, Is.EqualTo(showtime.StartTime));
            Assert.That(showtimeViewModel.CinemaName, Is.EqualTo(cinema.Name));
            Assert.That(showtimeViewModel.MovieName, Is.EqualTo(movie.Title));
        }

    }
}
