namespace CinemaSystem.Services.Tests
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Cinema;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using static CinemaSystem.Common.GeneralApplicationConstants;

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
        public async Task DeleteCinemaAsync_Should_Delete_Cinema_And_Remove_Cache()
        {
            // Arrange
            var cinema = new Cinema
            {
                Name = "Test Cinema",
                ImageUrl = "test.jpg",
                Address = "123 Test Street"
            };
            dbContext.Cinemas.Add(cinema);
            await dbContext.SaveChangesAsync();

            // Act
            await cinemaService.DeleteCinemaAsync(cinema.Id.ToString());

            // Assert
            memoryCacheMock.Verify(m => m.Remove(CinemaCacheValue), Times.Once());
        }
    }
}