namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Ticket;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public class TicketService : ITicketService
    {
        private readonly CinemaSystemDbContext dbContext;
        private readonly IShowtimeService showtimeService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<TicketService> logger;

        public TicketService(CinemaSystemDbContext dbContext, IShowtimeService showtimeService, UserManager<ApplicationUser> userManager, ILogger<TicketService> logger)
        {
            this.dbContext = dbContext;
            this.showtimeService = showtimeService;
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task AddTicketAsync(TicketAddEditViewModel model)
        {
            Ticket ticket = new Ticket
            {
                ShowtimeId = model.ShowtimeId,
                UserId = Guid.Parse(model.UserId),
                Price = model.TicketPrice,
                SeatNumber = model.SeatNumber.ToString()
            };

            await dbContext.Tickets.AddAsync(ticket);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(string id)
        {
            var ticket = await dbContext.Tickets
           .FirstOrDefaultAsync(t => t.Id.ToString() == id);

            if (ticket != null)
            {
                ticket.isActive = false;
            }
            else
            {
                string error = "Ticket could not be found in the database!";
                logger.LogError(error);
                throw new InvalidOperationException(error);
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task EditTicketAsync(string id, TicketAddEditViewModel model)
        {
            var ticket = await dbContext.Tickets
               .Where(sh => sh.isActive)
               .FirstOrDefaultAsync(sh => sh.Id.ToString() == id);

            if (ticket != null)
            {
                ticket.Price = model.TicketPrice;
                ticket.SeatNumber = model.SeatNumber.ToString();
                ticket.ShowtimeId = model.ShowtimeId;
                ticket.UserId = Guid.Parse(model.UserId);

                await dbContext.SaveChangesAsync();
            }
            else
            {
                string error = "Ticket could not be found in the database!";
                logger.LogError(error);
                throw new InvalidOperationException(error);
            }
        }

        public async Task<TicketAddEditViewModel?> GetAddTicketModelAsync()
        {
            var model = new TicketAddEditViewModel();
            var showtimes = await showtimeService.GetShowtimesAsync();
            var allUsers = await userManager.Users.ToListAsync();
            model.Showtimes = showtimes.ToDictionary(s => s.Id, s => s.CinemaName + " - " + s.MovieName + " - " + s.StartTime.ToString());
            model.Users = allUsers.ToDictionary(user => user.Id.ToString(), user => user.UserName);
            return model;
        }

        public async Task<TicketAddEditViewModel?> GetEditTicketModelAsync(string id)
        {
            var showtimes = await showtimeService.GetShowtimesAsync();
            var allUsers = await userManager.Users.ToListAsync();

            TicketAddEditViewModel? ticketAddEditViewModel = await dbContext.Tickets
                    .Where(t => t.Id.ToString() == id)
                    .Select(t => new TicketAddEditViewModel
                    {
                        TicketPrice = t.Price,
                        SeatNumber = int.Parse(t.SeatNumber),
                        UserId = t.UserId.ToString(),
                        ShowtimeId = t.ShowtimeId
                    }).FirstOrDefaultAsync();

            if (ticketAddEditViewModel != null)
            {
                ticketAddEditViewModel.Users = allUsers.ToDictionary(user => user.Id.ToString(), user => user.UserName);
                ticketAddEditViewModel.Showtimes = showtimes.ToDictionary(s => s.Id, s => s.CinemaName + " - " + s.MovieName + " - " + s.StartTime.ToString());
            }
            return ticketAddEditViewModel;
        }

        public async Task<IEnumerable<int>> GetSelectedSeatsAsync(string showtimeId)
        {
            Showtime? showtime = await dbContext.Showtimes
             .Where(s => s.Id.ToString() == showtimeId)
             .Include(s => s.Tickets)
             .FirstOrDefaultAsync();

            if (showtime != null)
            {
                return showtime.Tickets.Select(t => int.Parse(t.SeatNumber)).ToList();
            }
            else
            {
                string error = "Showtime could not be found in the database!";
                logger.LogError(error);
                throw new InvalidOperationException(error);
            }
        }

        public async Task<IEnumerable<TicketViewModel>> GetTicketsAsync()
        {
            return await dbContext.Tickets
             .Where(t => t.isActive)
             .OrderBy(t => t.User.UserName)
             .Select(t => new TicketViewModel
             {
                 Id = t.Id.ToString(),
                 Price = t.Price,
                 SeatNumber = t.SeatNumber,
                 Username = t.User.UserName,
                 Showtime = t.Showtime.Cinema.Name + " - " + t.Showtime.Movie.Title + " - " + t.Showtime.StartTime.ToString("dd MM yyyy HH:mm")
             }).ToListAsync();
        }

        public async Task ReserveTicketsAsync(string showtimeId, string usedId, List<int> selectedSeats)
        {
            Showtime? showtime = await dbContext.Showtimes
            .Include(s => s.Tickets)
            .FirstOrDefaultAsync(s => s.Id.ToString() == showtimeId);

            if (showtime == null)
            {
                throw new InvalidOperationException("Showtime not found");
            }
            foreach (var seat in selectedSeats)
            {
                if (dbContext.Tickets.Any(t => t.ShowtimeId.ToString() == showtimeId && t.SeatNumber == seat.ToString()))
                {
                    throw new InvalidOperationException("Seat already reserved!");
                }

                Ticket ticket = new Ticket
                {
                    Showtime = showtime,
                    Price = showtime.TicketPrice,
                    UserId = Guid.Parse(usedId),
                    SeatNumber = seat.ToString(),
                };
                dbContext.Tickets.Add(ticket);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
