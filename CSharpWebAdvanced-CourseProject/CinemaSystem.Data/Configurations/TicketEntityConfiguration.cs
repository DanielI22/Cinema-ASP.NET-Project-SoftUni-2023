namespace CinemaSystem.Data.Configurations
{
    using CinemaSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
