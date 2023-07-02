using CinemaSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSystem.Data.Configurations
{
    public class MovieGenreEntityConfiguration : IEntityTypeConfiguration<MovieGenre>
    {
        public void Configure(EntityTypeBuilder<MovieGenre> builder)
        {
            builder
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            builder
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(this.GenerateMovieGenres());

        }

        private MovieGenre[] GenerateMovieGenres()
        {
            ICollection<MovieGenre> movieGenres = new HashSet<MovieGenre>();

            MovieGenre movieGenre;

            movieGenre = new MovieGenre()
            {
                MovieId = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                GenreId = 1
            };
            movieGenres.Add(movieGenre);

            movieGenre = new MovieGenre()
            {
                MovieId = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                GenreId = 2
            };
            movieGenres.Add(movieGenre);

            movieGenre = new MovieGenre()
            {
                MovieId = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                GenreId = 3
            };
            movieGenres.Add(movieGenre);

            movieGenre = new MovieGenre()
            {
                MovieId = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                GenreId = 4
            };
            movieGenres.Add(movieGenre);


            return movieGenres.ToArray();
        }
    }
}
