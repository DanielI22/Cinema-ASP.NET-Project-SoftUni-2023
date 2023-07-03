namespace CinemaSystem.Data.Configurations
{
    using CinemaSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;

    public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(this.GenerateGenres());
        }

        private Genre[] GenerateGenres()
        {
            ICollection<Genre> genres = new HashSet<Genre>();

            Genre genre;

            genre = new Genre()
            {
                Id = 1,
                Name = "Biography"
            };
            genres.Add(genre);

            genre = new Genre()
            {
                Id = 2,
                Name = "Drama"
            };
            genres.Add(genre);

            genre = new Genre()
            {
                Id = 3,
                Name = "Action"
            };
            genres.Add(genre);

            genre = new Genre()
            {
                Id = 4,
                Name = "Adventure"
            };
            genres.Add(genre);

            return genres.ToArray();
        }
    }
}
