﻿namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Ticket;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class TicketService : ITicketService
    {
        private readonly CinemaSystemDbContext dbContext;

        public TicketService(CinemaSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<int>> GetSelectedSeatsAsync(int showtimeId)
        {
            Showtime? showtime = await dbContext.Showtimes
             .Where(s => s.Id == showtimeId)
             .Include(s => s.Tickets)
             .FirstOrDefaultAsync();

            if (showtime != null)
            {
                return showtime.Tickets.Select(t => int.Parse(t.SeatNumber)).ToList();
            }

            return new List<int>();
        }

        public async Task ReserveTicketsAsync(int showtimeId, Guid usedId, List<int> selectedSeats)
        {
            Showtime? showtime = await dbContext.Showtimes
            .Include(s => s.Tickets)
            .FirstOrDefaultAsync(s => s.Id == showtimeId);

            if (showtime == null)
            {
                throw new InvalidOperationException("Showtime not found"); // Showtime not found
            }
            foreach (var seat in selectedSeats)
            {
                Ticket ticket = new Ticket
                {
                    Showtime = showtime,
                    Price = showtime.TicketPrice,
                    UserId = usedId,
                    SeatNumber = seat.ToString(),
                };
                dbContext.Tickets.Add(ticket);
            }

            // Save changes to the database
            await dbContext.SaveChangesAsync();
        }
    }
}
