using CinemaSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CinemaSystem.Web.Data
{
    public class CinemaSystemDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public CinemaSystemDbContext(DbContextOptions<CinemaSystemDbContext> options)
            : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Cinema> Cinemas { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<Showtime> Showtimes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(CinemaSystemDbContext)) ??
                Assembly.GetExecutingAssembly();

            modelBuilder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}