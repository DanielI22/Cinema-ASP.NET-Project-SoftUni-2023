namespace CinemaSystem.Services.Tests
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Services.Data;
    using CinemaSystem.Web.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using CinemaSystem.Data.Models;
    using CinemaSystem.Web.ViewModels.Movie;
    using System.Net;

    [TestFixture]
    public class MovieServiceTests
    {
        private MovieService movieService;
        private CinemaSystemDbContext dbContext;
        private Mock<IReviewService> reviewServiceMock;
        private Mock<IConfiguration> configurationMock;
        private Mock<ILogger<MovieService>> loggerMock;
        private IMemoryCache memoryCache;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CinemaSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            dbContext = new CinemaSystemDbContext(options);
            reviewServiceMock = new Mock<IReviewService>();
            configurationMock = new Mock<IConfiguration>();
            loggerMock = new Mock<ILogger<MovieService>>();
            memoryCache = new MemoryCache(new MemoryCacheOptions());

            movieService = new MovieService(dbContext, reviewServiceMock.Object, configurationMock.Object, loggerMock.Object, memoryCache);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            memoryCache.Dispose();
        }

        [Test]
        public async Task GetMovieCardsForMovieIdsAsync_ValidIds_ReturnsCorrectViewModels()
        {
            // Arrange
            var id1 = Guid.NewGuid().ToString();
            var id2 = Guid.NewGuid().ToString();
            var movieIds = new List<string> { id1, id2 };
            dbContext.Movies.AddRange(new[]
            {
                new Movie { Id = Guid.Parse(id1), Title = "Movie 1", PosterImageUrl = "poster1.jpg", Description="Test" },
                new Movie { Id = Guid.Parse(id2), Title = "Movie 2", PosterImageUrl = "poster2.jpg",  Description="Test"},
                new Movie { Id = Guid.NewGuid(), Title = "Movie 3", PosterImageUrl = "poster3.jpg",  Description="Test"}
            });
            await dbContext.SaveChangesAsync();

            // Act
            var movieCards = await movieService.GetMovieCardsForMovieIdsAsync(movieIds);

            // Assert
            Assert.That(movieCards.Count(), Is.EqualTo(2));
            Assert.IsTrue(movieCards.All(mc => movieIds.Contains(mc.Id.ToString())));
        }


        [Test]
        public async Task FilterMoviesAsync_SearchNameOnly_ReturnsFilteredMovies()
        {
            // Arrange
            dbContext.Movies.AddRange(new[]
            {
        new Movie { Id = Guid.NewGuid(), Title = "The Matrix", Description="Test" },
        new Movie { Id = Guid.NewGuid(), Title = "Inception", Description="Test" },
        new Movie { Id = Guid.NewGuid(), Title = "Avatar", Description="Test" }
    });
            await dbContext.SaveChangesAsync();

            // Act
            var filteredMovies = await movieService.FilterMoviesAsync("Matrix", 0);

            // Assert
            Assert.That(filteredMovies.Count(), Is.EqualTo(1));
            Assert.That(filteredMovies.First().Title, Is.EqualTo("The Matrix"));
        }

        [Test]
        public async Task GetAllMoviesCardAsync_NoCache_ReturnsAllMovies()
        {
            // Arrange
            dbContext.Movies.AddRange(new[]
            {
        new Movie { Id = Guid.NewGuid(), Title = "Movie 1", Description="Test" },
        new Movie { Id = Guid.NewGuid(), Title = "Movie 2", Description="Test" },
        new Movie { Id = Guid.NewGuid(), Title = "Movie 3", Description="Test" }
    });
            await dbContext.SaveChangesAsync();

            // Act
            var allMovies = await movieService.GetAllMoviesCardAsync();

            // Assert
            Assert.That(allMovies.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task GetMovieDetailsModelAsync_ValidId_ReturnsMovieDetailsViewModel()
        {
            // Arrange
            var movieId = Guid.NewGuid();
            dbContext.Movies.Add(new Movie
            {
                Id = movieId,
                Title = "The Matrix",
                Description = "Sci-fi movie",
                ReleaseYear = 1999,
                PosterImageUrl = "matrix.jpg",
                Reviews = new List<Review>()
            });
            await dbContext.SaveChangesAsync();

            // Act
            var movieDetails = await movieService.GetMovieDetailsModelAsync(movieId.ToString(), 1, 10);

            // Assert
            Assert.IsNotNull(movieDetails);
            Assert.That(movieDetails.Title, Is.EqualTo("The Matrix"));
            Assert.That(movieDetails.Description, Is.EqualTo("Sci-fi movie"));
            Assert.That(movieDetails.ReleaseYear, Is.EqualTo(1999));
        }

        [Test]
        public async Task AddMovieAsync_ValidModel_AddsMovieToDatabase()
        {
            // Arrange
            var model = new MovieAddEditViewModel
            {
                Title = "Test Movie",
                Description = "Test Description",
                ReleaseYear = 2023,
                GenresId = new List<int> { 1, 2 }, // Assuming genre IDs
            };

            // Act
            await movieService.AddMovieAsync(model);

            // Assert
            var addedMovie = await dbContext.Movies.FirstOrDefaultAsync(m => m.Title == "Test Movie");
            Assert.IsNotNull(addedMovie);
            Assert.That(addedMovie.Description, Is.EqualTo("Test Description"));
        }

        [Test]
        public async Task EditMovieAsync_ValidIdAndModel_EditsMovieInDatabase()
        {
            // Arrange
            var movieId = Guid.NewGuid();
            dbContext.Movies.Add(new Movie { Id = movieId, Title = "Old Title", Description = "Test" });
            await dbContext.SaveChangesAsync();

            var model = new MovieAddEditViewModel { Title = "New Title", GenresId = new List<int>() };

            // Act
            await movieService.EditMovieAsync(movieId.ToString(), model);

            // Assert
            var editedMovie = await dbContext.Movies.FindAsync(movieId);
            Assert.IsNotNull(editedMovie);
            Assert.That(editedMovie.Title, Is.EqualTo("New Title"));
        }

        [Test]
        public async Task DeleteMovieAsync_ValidId_DeletesMovieFromDatabase()
        {
            // Arrange
            var movieId = Guid.NewGuid();
            dbContext.Movies.Add(new Movie { Id = movieId, Title = "Test Movie", Description = "Test" });
            await dbContext.SaveChangesAsync();

            // Act
            await movieService.DeleteMovieAsync(movieId.ToString());

            // Assert
            var deletedMovie = await dbContext.Movies.FindAsync(movieId);
            Assert.IsFalse(deletedMovie?.isActive);
        }

        [Test]
        public async Task GetMovieCardsForMovieIdsAsync_ValidMovieIds_ReturnsCorrectMovieCards()
        {
            // Arrange
            var movie1 = new Movie { Id = Guid.NewGuid(), Title = "Movie 1", Description = "Test" };
            var movie2 = new Movie { Id = Guid.NewGuid(), Title = "Movie 2", Description = "Test" };
            dbContext.Movies.AddRange(movie1, movie2);
            await dbContext.SaveChangesAsync();

            var movieIds = new List<string> { movie1.Id.ToString(), movie2.Id.ToString() };

            // Act
            var movieCards = await movieService.GetMovieCardsForMovieIdsAsync(movieIds);

            // Assert
            Assert.That(movieCards.Count(), Is.EqualTo(2));
            Assert.IsTrue(movieCards.Any(mc => mc.Title == "Movie 1"));
            Assert.IsTrue(movieCards.Any(mc => mc.Title == "Movie 2"));
        }

        [Test]
        public async Task FilterMoviesAsync_ValidSearchParams_ReturnsFilteredMovies()
        {
            // Arrange
            dbContext.Movies.Add(new Movie { Id = Guid.NewGuid(), Title = "Action Movie", Description = "Action film", ReleaseYear = 2022 });
            dbContext.Movies.Add(new Movie { Id = Guid.NewGuid(), Title = "Comedy Movie", Description = "Comedy film", ReleaseYear = 2021 });
            await dbContext.SaveChangesAsync();

            // Act
            var filteredMovies = await movieService.FilterMoviesAsync("Action", 0);

            // Assert
            Assert.That(filteredMovies.Count(), Is.EqualTo(1));
            Assert.IsTrue(filteredMovies.All(m => m.Title.Contains("Action", StringComparison.OrdinalIgnoreCase)));
            // Additional assertions for other properties...
        }

        [Test]
        public async Task GetAllShowtimeMoviesAsync_ReturnsAllActiveMovies()
        {
            // Arrange
            dbContext.Movies.Add(new Movie { Id = Guid.NewGuid(), Title = "Movie 1", Description = "Test", isActive = true });
            dbContext.Movies.Add(new Movie { Id = Guid.NewGuid(), Title = "Movie 2", Description = "Test", isActive = false });
            await dbContext.SaveChangesAsync();

            // Act
            var showtimeMovies = await movieService.GetAllShowtimeMoviesAsync();

            // Assert
            Assert.That(showtimeMovies.Count(), Is.EqualTo(1));
            Assert.IsTrue(showtimeMovies.Any(sm => sm.Title == "Movie 1"));
        }

        [Test]
        public async Task GetEditMovieModelAsync_ValidId_ReturnsMovieAddEditViewModel()
        {
            // Arrange
            var movieId = Guid.NewGuid().ToString();
            var genres = new List<Genre>
    {
        new Genre { Id = 1, Name = "Action", isActive = true },
        new Genre { Id = 2, Name = "Drama", isActive = true },
    };

            var movies = new List<Movie>
    {
        new Movie
        {
            Id = Guid.Parse(movieId),
            Title = "Test Movie",
            PosterImageUrl = "poster.jpg",
            Description = "Test description",
            ReleaseYear = 2023,
            isActive = true,
            MovieGenres = new List<MovieGenre>
            {
                new MovieGenre { GenreId = 1 },
                new MovieGenre { GenreId = 2 }
            }
        }
    };

            dbContext.Genres.AddRange(genres);
            dbContext.Movies.AddRange(movies);
            dbContext.SaveChanges();

            // Act
            var result = await movieService.GetEditMovieModelAsync(movieId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Title, Is.EqualTo("Test Movie"));
            Assert.That(result.PosterImageUrl, Is.EqualTo("poster.jpg"));
            Assert.That(result.Description, Is.EqualTo("Test description"));
            Assert.That(result.ReleaseYear, Is.EqualTo(2023));
            CollectionAssert.AreEqual(new List<int> { 1, 2 }, result.GenresId);
        }
    }
}
