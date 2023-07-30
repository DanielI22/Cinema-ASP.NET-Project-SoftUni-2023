// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using CinemaSystem.Web.Data;
using CinemaSystem.Web.Infrastructure.Extensions;
using CinemaSystem.Web.ViewModels.Movie;
using CinemaSystem.Web.ViewModels.Ticket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Web.Areas.Identity.Pages.Account.Manage
{
    public class UserTicketsDataModel : PageModel
    {
        private readonly CinemaSystemDbContext dbContext;

        public UserTicketsDataModel(CinemaSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<UserTicketViewModel> UserTickets { get; private set; } = null!;

        public async Task<IActionResult> OnGet()
        {
            string? userId = User.GetId();

            UserTickets = await dbContext.Tickets
                .Where(t => t.UserId.ToString() == userId && t.isActive)
                .OrderBy(t => t.Showtime.StartTime)
                .Include(t => t.Showtime)
                .ThenInclude(s => s.Movie)
                .ThenInclude(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Select(t => new UserTicketViewModel
                {
                    TicketPrice = t.Price,
                    CinemaName = t.Showtime.Cinema.Name,
                    SeatNumber = t.SeatNumber,
                    ShowtimeStartTime = t.Showtime.StartTime,
                    Movie = new MovieCardViewModel
                    {
                        Id = t.Showtime.Movie.Id,
                        Title = t.Showtime.Movie.Title,
                        PosterUrl = t.Showtime.Movie.PosterImageUrl,
                        Genres = t.Showtime.Movie.MovieGenres.Select(mg => new GenreViewModel
                        {
                            Id = mg.GenreId,
                            Name = mg.Genre.Name
                        })
                    }

                }).ToListAsync();

            return Page();
        }
    }
}
