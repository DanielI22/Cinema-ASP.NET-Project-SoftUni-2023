namespace CinemaSystem.Services.Tests.UnitTests
{
    using CinemaSystem.Services.Data;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Cinema;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using static CinemaSystem.Common.GeneralApplicationConstants;
    using Cinema = CinemaSystem.Data.Models.Cinema;
    using Showtime = CinemaSystem.Data.Models.Showtime;

    [TestFixture]
    public class CinemaServiceTests
    {
        private CinemaSystemDbContext dbContext;
        private Mock<IMemoryCache> memoryCacheMock;
        private CinemaService cinemaService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CinemaSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            dbContext = new CinemaSystemDbContext(options);
            memoryCacheMock = new Mock<IMemoryCache>();
            var loggerMock = new Mock<ILogger<CinemaService>>();

            cinemaService = new CinemaService(dbContext, loggerMock.Object, memoryCacheMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
        }
        [Test]
        public async Task AddCinemaAsync_Should_Add_Cinema_And_Remove_Cache()
        {
            // Arrange
            var model = new CinemaAddEditViewModel
            {
                Name = "Test Cinema",
                ImageUrl = "test.jpg",
                Address = "123 Test Street"
            };

            // Act
            await cinemaService.AddCinemaAsync(model);

            // Assert
            Assert.That(dbContext.Cinemas.Count().Equals(1));
            memoryCacheMock.Verify(m => m.Remove(CinemaCacheValue), Times.Once());
        }
        [Test]
        public async Task DeleteCinemaAsync_CinemaExists_SetIsActiveToFalse()
        {
            // Arrange
            var cinemaId = 1;
            var cinema = new Cinema { Id = cinemaId, Address = "New", Name = "Name", isActive = true };
            dbContext.Cinemas.Add(cinema);
            dbContext.SaveChanges();

            // Act
            await cinemaService.DeleteCinemaAsync(cinemaId.ToString());

            // Assert
            var deletedCinema = await dbContext.Cinemas.FindAsync(cinemaId);
            Assert.IsFalse(deletedCinema?.isActive);
        }

        [Test]
        public async Task EditCinemaAsync_CinemaExists_UpdateCinemaProperties()
        {
            // Arrange
            var cinemaId = 1;
            var originalName = "Old Name";
            var originalAddress = "Old Address";
            var originalImageUrl = "old_image_url.jpg";
            var updatedName = "New Name";
            var updatedAddress = "New Address";
            var updatedImageUrl = "new_image_url.jpg";

            var cinema = new Cinema
            {
                Id = cinemaId,
                Name = originalName,
                Address = originalAddress,
                ImageUrl = originalImageUrl
            };
            dbContext.Cinemas.Add(cinema);
            dbContext.SaveChanges();

            var model = new CinemaAddEditViewModel
            {
                Name = updatedName,
                Address = updatedAddress,
                ImageUrl = updatedImageUrl
            };

            // Act
            await cinemaService.EditCinemaAsync(cinemaId.ToString(), model);

            // Assert
            var editedCinema = await dbContext.Cinemas.FindAsync(cinemaId);
            Assert.That(editedCinema?.Name, Is.EqualTo(updatedName));
            Assert.That(editedCinema.Address, Is.EqualTo(updatedAddress));
            Assert.That(editedCinema.ImageUrl, Is.EqualTo(updatedImageUrl));
        }

        [Test]
        public async Task GetCinemaAvailableDatesAsync_ReturnsDistinctDates()
        {
            // Arrange
            var cinemaId = 1;
            var expectedDates = new List<DateTime>
    {
        DateTime.Today,
        DateTime.Today.AddDays(1),
        DateTime.Today.AddDays(2)
    };

            var cinema = new Cinema
            {
                Id = cinemaId,
                Name = "Test Cinema",
                Address = "Test Address",
                ImageUrl = "test.jpg",
                isActive = true
            };

            dbContext.Cinemas.Add(cinema);

            var showtimes = expectedDates.Select(date => new Showtime
            {
                Id = new int(),
                CinemaId = cinemaId,
                StartTime = date.AddHours(18),
                isActive = true
            }).ToList();

            dbContext.Showtimes.AddRange(showtimes);
            dbContext.SaveChanges();

            // Act
            var result = await cinemaService.GetCinemaAvailableDatesAsync(cinemaId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(expectedDates.Count));
            Assert.That(result, Is.EquivalentTo(expectedDates));
        }

        [Test]
        public async Task GetEditCinemaModelAsync_ReturnsCorrectCinemaViewModel()
        {
            // Arrange
            var cinemaId = 1;
            var cinema = new Cinema
            {
                Id = cinemaId,
                Name = "Cinema 1",
                Address = "Address 1",
                ImageUrl = "image1.jpg",
                isActive = true
            };

            dbContext.Cinemas.Add(cinema);
            dbContext.SaveChanges();

            // Act
            var result = await cinemaService.GetEditCinemaModelAsync(cinemaId.ToString());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(cinema.Name));
            Assert.That(result.Address, Is.EqualTo(cinema.Address));
            Assert.That(result.ImageUrl, Is.EqualTo(cinema.ImageUrl));
        }
    }
}