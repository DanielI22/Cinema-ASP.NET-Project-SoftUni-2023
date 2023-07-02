using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaSystem.Data.Models;

namespace CinemaSystem.Data.Configurations
{
    public class ShowtimeEntityConfiguration : IEntityTypeConfiguration<Showtime>
    {
        public void Configure(EntityTypeBuilder<Showtime> builder)
        {
            builder
                .HasOne(st => st.Movie)
                .WithMany(m => m.Showtimes)
                .HasForeignKey(st => st.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(st => st.Cinema)
               .WithMany(c => c.Showtimes)
               .HasForeignKey(st => st.CinemaId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
