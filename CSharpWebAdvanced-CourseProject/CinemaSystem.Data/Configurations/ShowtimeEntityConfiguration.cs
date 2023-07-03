namespace CinemaSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using CinemaSystem.Data.Models;

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

            builder.HasData(this.GenerateShowtimes());
        }

        private Showtime[] GenerateShowtimes()
        {
            ICollection<Showtime> showtimes = new HashSet<Showtime>();

            Showtime showtime;

            showtime = new Showtime()
            {
                Id = 1,
                CinemaId = 1,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 2, hour: 12, minute: 30, second: 0),
                TicketPrice = 15,
                MovieId = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 2,
                CinemaId = 1,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 2, hour: 18, minute: 30, second: 0),
                TicketPrice = 15,
                MovieId = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 3,
                CinemaId = 1,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 3, hour: 12, minute: 30, second: 0),
                TicketPrice = 12,
                MovieId = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 4,
                CinemaId = 1,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 3, hour: 18, minute: 30, second: 0),
                TicketPrice = 12,
                MovieId = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 5,
                CinemaId = 2,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 2, hour: 12, minute: 30, second: 0),
                TicketPrice = 15,
                MovieId = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 6,
                CinemaId = 2,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 2, hour: 18, minute: 30, second: 0),
                TicketPrice = 15,
                MovieId = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 7,
                CinemaId = 2,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 3, hour: 12, minute: 30, second: 0),
                TicketPrice = 12,
                MovieId = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 8,
                CinemaId = 2,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 3, hour: 18, minute: 30, second: 0),
                TicketPrice = 12,
                MovieId = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 9,
                CinemaId = 3,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 2, hour: 12, minute: 30, second: 0),
                TicketPrice = 15,
                MovieId = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 10,
                CinemaId = 3,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 2, hour: 18, minute: 30, second: 0),
                TicketPrice = 15,
                MovieId = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 11,
                CinemaId = 3,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 3, hour: 12, minute: 30, second: 0),
                TicketPrice = 12,
                MovieId = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 12,
                CinemaId = 3,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 3, hour: 18, minute: 30, second: 0),
                TicketPrice = 12,
                MovieId = Guid.Parse("ab758330-8d53-4c59-b77c-bca379c1d8b7"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 13,
                CinemaId = 1,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 2, hour: 12, minute: 30, second: 0),
                TicketPrice = 15,
                MovieId = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 14,
                CinemaId = 1,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 2, hour: 18, minute: 30, second: 0),
                TicketPrice = 15,
                MovieId = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 15,
                CinemaId = 1,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 3, hour: 12, minute: 30, second: 0),
                TicketPrice = 12,
                MovieId = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 16,
                CinemaId = 1,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 3, hour: 18, minute: 30, second: 0),
                TicketPrice = 12,
                MovieId = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 17,
                CinemaId = 2,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 2, hour: 12, minute: 30, second: 0),
                TicketPrice = 15,
                MovieId = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 18,
                CinemaId = 2,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 2, hour: 18, minute: 30, second: 0),
                TicketPrice = 15,
                MovieId = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 19,
                CinemaId = 2,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 3, hour: 12, minute: 30, second: 0),
                TicketPrice = 12,
                MovieId = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 20,
                CinemaId = 2,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 3, hour: 18, minute: 30, second: 0),
                TicketPrice = 12,
                MovieId = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 21,
                CinemaId = 3,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 2, hour: 12, minute: 30, second: 0),
                TicketPrice = 15,
                MovieId = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 22,
                CinemaId = 3,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 2, hour: 18, minute: 30, second: 0),
                TicketPrice = 15,
                MovieId = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 23,
                CinemaId = 3,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 3, hour: 12, minute: 30, second: 0),
                TicketPrice = 12,
                MovieId = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),

            };
            showtimes.Add(showtime);

            showtime = new Showtime()
            {
                Id = 24,
                CinemaId = 3,
                StartTime = new DateTime(year: DateTime.Now.Year, month: 9, day: 3, hour: 18, minute: 30, second: 0),
                TicketPrice = 12,
                MovieId = Guid.Parse("a622d82d-aed0-44d9-9f4c-577418ca1172"),

            };
            showtimes.Add(showtime);

            return showtimes.ToArray();
        }
    }
}
