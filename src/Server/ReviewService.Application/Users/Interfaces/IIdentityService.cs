using ReviewService.Application.Common.Models;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.Users.Interfaces
{
    public interface IIdentityService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(string fullName, string userName, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
