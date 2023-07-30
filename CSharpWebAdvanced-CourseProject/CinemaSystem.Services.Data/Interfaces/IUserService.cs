namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Web.ViewModels.Ticket;
    using CinemaSystem.Web.ViewModels.User;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<IdentityResult> AddUserAsync(UserAddViewModel user);
        Task DeleteUserAsync(string id);
        Task<IdentityResult> EditUserAsync(string id, UserEditViewModel user);
        Task<UserEditViewModel?> GetEditUserModelAsync(string id);
        Task<IEnumerable<UserViewModel>> GetUsersAsync();
    }
}
