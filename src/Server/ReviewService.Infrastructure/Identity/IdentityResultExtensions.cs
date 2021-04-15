using Microsoft.AspNetCore.Identity;
using ReviewService.Application.Common.Models;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Linq;

namespace ReviewService.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
        public static List<User> ToApplicationUsers(this IEnumerable<ApplicationUser> users) 
        {
            List<User> applicationUsers = new List<User>();
            foreach (var user in users)
            {
                applicationUsers.Add(new User 
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    UserName = user.UserName
                });
            }
            return applicationUsers;
        }
    }
}
