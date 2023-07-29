namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Web.Data;
    using Microsoft.AspNetCore.Identity;

    public class UserService
    {
        private readonly CinemaSystemDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(CinemaSystemDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }


    }
}
