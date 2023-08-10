namespace CinemaSystem.Services.Tests.UnitTests
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Genre;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;

    [TestFixture]
    public class GenreServiceTests
    {
        private DbContextOptions<CinemaSystemDbContext> dbContextOptions;
        private CinemaSystemDbContext dbContext;
        private GenreService genreService;

        [SetUp]
        public void Setup()
        {
            dbContextOptions = new DbContextOptionsBuilder<CinemaSystemDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new CinemaSystemDbContext(dbContextOptions);

            var loggerMock = new Mock<ILogger<GenreService>>();

            genreService = new GenreService(dbContext, loggerMock.Object);
        }

        [Test]
        public async Task AddGenreAsync_ShouldAddGenre()
        {
            // Arrange
            var genre = new GenreAddEditViewModel
            {
                Name = "Action"
            };

            // Act
            await genreService.AddGenreAsync(genre);

            // Assert
            var addedGenre = dbContext.Genres.FirstOrDefault();
            Assert.IsNotNull(addedGenre);
            Assert.That(addedGenre?.Name, Is.EqualTo("Action"));
        }

        [Test]
        public async Task DeleteGenreAsync_ShouldSetIsActiveToFalse()
        {
            // Arrange
            var genre = new Genre
            {
                Name = "Comedy",
                isActive = true
            };
            dbContext.Genres.Add(genre);
            dbContext.SaveChanges();

            // Act
            await genreService.DeleteGenreAsync(genre.Id.ToString());

            // Assert
            var deletedGenre = dbContext.Genres.Find(genre.Id);
            Assert.IsFalse(deletedGenre?.isActive);
        }

        [Test]
        public async Task EditGenreAsync_ShouldUpdateGenre()
        {
            // Arrange
            var genre = new Genre
            {
                Name = "Drama",
                isActive = true
            };
            dbContext.Genres.Add(genre);
            dbContext.SaveChanges();

            var updatedGenre = new GenreAddEditViewModel
            {
                Name = "Romance"
            };

            // Act
            await genreService.EditGenreAsync(genre.Id.ToString(), updatedGenre);

            // Assert
            var editedGenre = dbContext.Genres.Find(genre.Id);
            Assert.That(editedGenre?.Name, Is.EqualTo("Romance"));
        }

        [Test]
        public async Task GetEditGenreModelAsync_ShouldReturnGenreViewModel()
        {
            // Arrange
            var genre = new Genre
            {
                Name = "Sci-Fi",
                isActive = true
            };
            dbContext.Genres.Add(genre);
            dbContext.SaveChanges();

            // Act
            var genreViewModel = await genreService.GetEditGenreModelAsync(genre.Id.ToString());

            // Assert
            Assert.IsNotNull(genreViewModel);
            Assert.That(genreViewModel?.Name, Is.EqualTo("Sci-Fi"));
        }

        [Test]
        public async Task GetGenresAsync_ShouldReturnListOfGenreViewModels()
        {
            // Arrange
            dbContext.Genres.AddRange(new[]
            {
                new Genre { Name = "Horror", isActive = true },
                new Genre { Name = "Action", isActive = true },
                new Genre { Name = "Comedy", isActive = true }
            });
            dbContext.SaveChanges();

            // Act
            var genreViewModels = await genreService.GetGenresAsync();

            // Assert
            Assert.IsNotNull(genreViewModels);
            Assert.That(genreViewModels.Count(), Is.EqualTo(3));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
