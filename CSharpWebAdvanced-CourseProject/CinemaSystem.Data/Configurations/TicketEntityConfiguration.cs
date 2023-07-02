using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CinemaSystem.Data.Models;

namespace CinemaSystem.Data.Configurations
{
    public class TicketEntityConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
                .HasOne(t => t.Showtime)
                .WithMany(st => st.Tickets)
                .HasForeignKey(t => t.ShowtimeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
