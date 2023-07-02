using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CinemaSystem.Data.Models;

namespace CinemaSystem.Data.Configurations
{
    public class CinemaEntityConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.HasData(this.GenerateCinemas());
        }

        private Cinema[] GenerateCinemas()
        {
            ICollection<Cinema> cinemas = new HashSet<Cinema>();

            Cinema cinema;

            cinema = new Cinema()
            {
                Id = 1,
                Name = "Cinemania Varna",
                Address = "Varna Dubrovnik 8",
                ImageUrl = "/cinemaImages/cinemaniaVarna.jpg"
            };
            cinemas.Add(cinema);

            cinema = new Cinema()
            {
                Id = 2,
                Name = "Cinemania Sofia",
                Address = "Sofia 33",
                ImageUrl = "/cinemaImages/cinemaniaSofia.jpg"
            };
            cinemas.Add(cinema);

            cinema = new Cinema()
            {
                Id = 3,
                Name = "Cinemania Plovid",
                Address = "Plovdiv 15",
                ImageUrl = "/cinemaImages/cinemaniaPlovdiv.jpg"
            };
            cinemas.Add(cinema);

            return cinemas.ToArray();
        }
    }
}
