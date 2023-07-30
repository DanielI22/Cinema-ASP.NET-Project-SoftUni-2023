namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Genre;
    using CinemaSystem.Web.ViewModels.Ticket;
    using CinemaSystem.Web.ViewModels.User;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static CinemaSystem.Common.EntityValidationConstants;
    using static CinemaSystem.Common.GeneralApplicationConstants;

    public class UserService : IUserService
    {
        private readonly CinemaSystemDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(CinemaSystemDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> AddUserAsync(UserAddViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if(result.Succeeded && model.IsAdmin)
            {
                await userManager.AddToRoleAsync(user, AdminRoleName);
            }

            return result;
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
               await userManager.DeleteAsync(user);
            }
        }

        public async Task<IdentityResult> EditUserAsync(string id, UserEditViewModel model)
        {
            var user = await userManager.FindByIdAsync(id) ?? throw new InvalidOperationException("User is not found");
            user.UserName = model.Username;
            user.Email = model.Email;

            var result = await userManager.UpdateAsync(user);

            if(!result.Succeeded)
            {
                return result;
            }

            if (!await userManager.IsInRoleAsync(user, AdminRoleName) && model.IsAdmin)
            {
                await userManager.AddToRoleAsync(user, AdminRoleName);
            }
            else if (await userManager.IsInRoleAsync(user, AdminRoleName) && !model.IsAdmin)
            {
                await userManager.RemoveFromRoleAsync(user, AdminRoleName);
            }

            if (model.Password != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                result = await userManager.ResetPasswordAsync(user, token, model.Password);
            }
            return result;
        }


        public async Task<UserEditViewModel?> GetEditUserModelAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var isAdmin = await userManager.IsInRoleAsync(user, AdminRoleName);
            var userModel = new UserEditViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                IsAdmin = isAdmin
            };
            return userModel;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var userModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                var isAdmin = await userManager.IsInRoleAsync(user, AdminRoleName);
                var userModel = new UserViewModel()
                {
                    Id = user.Id.ToString(),
                    Username = user.UserName,
                    Email = user.Email,
                    IsAdmin = isAdmin
                }; 
                userModels.Add(userModel);
            }

            return userModels;
        }
    }
}
