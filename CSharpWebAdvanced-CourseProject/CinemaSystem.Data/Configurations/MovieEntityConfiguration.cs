using CinemaSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSystem.Data.Configurations
{
    public class MovieEntityConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasData(this.GenerateMovies());
        }

        private Movie[] GenerateMovies()
        {
            ICollection<Movie> movies = new HashSet<Movie>();

            Movie movie;

            movie = new Movie()
            {
                Id = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                Title = "The Social Network",
                Description = "As Harvard student Mark Zuckerberg creates the social networking site that would become known as Facebook, he is sued by the twins who claimed he stole their idea and by the co-founder who was later squeezed out of the business.",
                ReleaseYear = 2010,
                PosterImageUrl = "https://m.media-amazon.com/images/M/MV5BOGUyZDUxZjEtMmIzMC00MzlmLTg4MGItZWJmMzBhZjE0Mjc1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SX300.jpg",
            };
            movies.Add(movie);

            movie = new Movie()
            {
                Id = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                Title = "Indiana Jones and the Dial of Destiny",
                Description = "Archaeologist Indiana Jones races against time to retrieve a legendary artifact that can change the course of history.",
                ReleaseYear = 2023,
                PosterImageUrl = "https://m.media-amazon.com/images/M/MV5BNDJhODYxYzItOGIwZC00ZTBiLTlmN2MtMjM2MzQyZDVkMGM4XkEyXkFqcGdeQXVyMTUzMDA3Mjc2._V1_SX300.jpg",
            };
            movies.Add(movie);

            return movies.ToArray();
        }
    }
}
